﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MAPE.Utils {
	/// <summary>
	/// The adapter class to abstract actual settings implementation.
	/// This implementation uses Json.NET as underlaying mechanism.
	/// </summary>
	public struct SettingsData {
		#region types

		public struct Value {
			#region data

			public static readonly Value NullValue = new Value(null);


			public readonly JToken InnerObject;

			#endregion


			#region properties

			public bool IsNull {
				get {
					return this.InnerObject == null;
				}
			}

			public JToken NonNullInnerObject {
				get {
					if (this.InnerObject == null) {
						throw new InvalidOperationException();
					}
					return this.InnerObject;
				}
			}

			#endregion


			#region creation and disposal

			public Value(JToken innerObject) {
				// argument checks
				// innerObject can be null

				// initialize members
				this.InnerObject = innerObject;
			}

			public static Value Parse(string jsonText) {
				return new Value(JToken.Parse(jsonText));
			}

			#endregion


			#region methods - get

			public string GetStringValue() {
				return (string)this.NonNullInnerObject;
			}

			public int GetInt32Value() {
				return (int)this.NonNullInnerObject;
			}

			public double GetDoubleValue() {
				return (double)this.NonNullInnerObject;
			}

			public bool GetBooleanValue() {
				return (bool)this.NonNullInnerObject;
			}

			public SettingsData GetObjectValue() {
				return new SettingsData((JObject)this.NonNullInnerObject);
			}

			public string[] GetStringArrayValue() {
				IEnumerable<JToken> tokens = (JArray)this.NonNullInnerObject;
				return (tokens == null)? null: tokens.Select(t => (string)t).ToArray();
			}

			public int[] GetInt32ArrayValue() {
				IEnumerable<JToken> tokens = (JArray)this.NonNullInnerObject;
				return (tokens == null) ? null : tokens.Select(t => (int)t).ToArray();
			}

			public double[] GetDoubleArrayValue() {
				IEnumerable<JToken> tokens = (JArray)this.NonNullInnerObject;
				return (tokens == null) ? null : tokens.Select(t => (double)t).ToArray();
			}

			public bool[] GetBooleanArrayValue() {
				IEnumerable<JToken> tokens = (JArray)this.NonNullInnerObject;
				return (tokens == null) ? null : tokens.Select(t => (bool)t).ToArray();
			}

			public SettingsData[] GetObjectArrayValue() {
				IEnumerable<JToken> tokens = (JArray)this.NonNullInnerObject;
				return (tokens == null)? null: tokens.Select(t => new SettingsData((JObject)t)).ToArray();
			}

			#endregion
		}

		#endregion


		#region data

		public static readonly SettingsData NullSettings = new SettingsData(null);

		public static readonly SettingsData[] EmptySettingsArray = new SettingsData[0];


		public readonly JObject InnerObject;

		#endregion


		#region properties

		public bool IsNull {
			get {
				return this.InnerObject == null;
			}
		}

		public JObject NonNullInnerObject {
			get {
				if (this.InnerObject == null) {
					throw new InvalidOperationException();
				}
				return this.InnerObject;
			}
		}

		#endregion


		#region creation and disposal

		private SettingsData(JObject innerObject) {
			// argument checks
			// innerObject can be null

			// initialize members
			this.InnerObject = innerObject;
		}

		public static SettingsData CreateEmptySettings() {
			return new SettingsData(new JObject());
		}

		public static SettingsData Parse(string jsonText) {
			JToken token = JToken.Parse(jsonText);
			return new SettingsData((JObject)token);
		}

		public SettingsData Clone() {
			JObject innerObject = this.InnerObject;
			if (innerObject != null) {
				innerObject = (JObject)innerObject.DeepClone();
			}

			return new SettingsData(innerObject);
		}

		#endregion


		#region methods - get value

		public Value GetValue(string settingName) {
			// argument checks
			if (settingName == null) {
				throw new ArgumentNullException(nameof(settingName));
			}

			JObject innerObject = this.InnerObject;
			JToken token = (innerObject == null) ? null : innerObject[settingName];
			return new Value(token);
		}


		public string GetStringValue(string settingName, string defaultValue, bool createIfNotExist = false) {
			Value value = GetValue(settingName);
			if (value.IsNull == false) {
				return value.GetStringValue();
			} else {
				if (createIfNotExist) {
					this.NonNullInnerObject[settingName] = defaultValue;
				}
				return defaultValue;
			}
		}

		public int GetInt32Value(string settingName, int defaultValue, bool createIfNotExist = false) {
			Value value = GetValue(settingName);
			if (value.IsNull == false) {
				return value.GetInt32Value();
			} else {
				if (createIfNotExist) {
					this.NonNullInnerObject[settingName] = defaultValue;
				}
				return defaultValue;
			}
		}

		public int? GetNullableInt32Value(string settingName) {
			Value value = GetValue(settingName);
			if (value.IsNull == false) {
				return value.GetInt32Value();
			} else {
				return null;
			}
		}

		public double GetDoubleValue(string settingName, double defaultValue, bool createIfNotExist = false) {
			Value value = GetValue(settingName);
			if (value.IsNull == false) {
				return value.GetDoubleValue();
			} else {
				if (createIfNotExist) {
					this.NonNullInnerObject[settingName] = defaultValue;
				}
				return defaultValue;
			}
		}

		public bool GetBooleanValue(string settingName, bool defaultValue, bool createIfNotExist = false) {
			Value value = GetValue(settingName);
			if (value.IsNull == false) {
				return value.GetBooleanValue();
			} else {
				if (createIfNotExist) {
					this.NonNullInnerObject[settingName] = defaultValue;
				}
				return defaultValue;
			}
		}

		public SettingsData GetObjectValue(string settingName, Func<SettingsData> defaultValueGenerator = null, bool createIfNotExist = false) {
			Value value = GetValue(settingName);
			if (value.IsNull == false) {
				return value.GetObjectValue();
			} else {
				SettingsData defaultValue = (defaultValueGenerator == null) ? SettingsData.NullSettings : defaultValueGenerator();
				if (createIfNotExist) {
					this.NonNullInnerObject[settingName] = defaultValue.InnerObject;
				}
				return defaultValue;
			}
		}

		public object GetEnumValue(string settingName, Type enumType, Enum defaultValue, bool createIfNotExist = false) {
			Value value = GetValue(settingName);
			if (value.IsNull == false) {
				return Enum.Parse(enumType, value.GetStringValue());
			} else {
				if (createIfNotExist) {
					this.NonNullInnerObject[settingName] = defaultValue.ToString();
				}
				return defaultValue;
			}
		}

		public IEnumerable<string> GetStringArrayValue(string settingName, IEnumerable<string> defaultValue, bool createIfNotExist = false) {
			Value value = GetValue(settingName);
			if (value.IsNull == false) {
				return value.GetStringArrayValue();
			} else {
				if (createIfNotExist) {
					if (defaultValue == null) {
						this.NonNullInnerObject[settingName] = (JToken)null;
					} else {
						this.NonNullInnerObject[settingName] = new JArray(defaultValue);
					}
				}
				return defaultValue;
			}
		}

		public IEnumerable<int> GetInt32ArrayValue(string settingName, IEnumerable<int> defaultValue, bool createIfNotExist = false) {
			Value value = GetValue(settingName);
			if (value.IsNull == false) {
				return value.GetInt32ArrayValue();
			} else {
				if (createIfNotExist) {
					if (defaultValue == null) {
						this.NonNullInnerObject[settingName] = (JToken)null;
					} else {
						this.NonNullInnerObject[settingName] = new JArray(defaultValue);
					}
				}
				return defaultValue;
			}
		}

		public IEnumerable<double> GetDoubleArrayValue(string settingName, IEnumerable<double> defaultValue, bool createIfNotExist = false) {
			Value value = GetValue(settingName);
			if (value.IsNull == false) {
				return value.GetDoubleArrayValue();
			} else {
				if (createIfNotExist) {
					if (defaultValue == null) {
						this.NonNullInnerObject[settingName] = (JToken)null;
					} else {
						this.NonNullInnerObject[settingName] = new JArray(defaultValue);
					}
				}
				return defaultValue;
			}
		}

		public IEnumerable<bool> GetBooleanArrayValue(string settingName, IEnumerable<bool> defaultValue, bool createIfNotExist = false) {
			Value value = GetValue(settingName);
			if (value.IsNull == false) {
				return value.GetBooleanArrayValue();
			} else {
				if (createIfNotExist) {
					if (defaultValue == null) {
						this.NonNullInnerObject[settingName] = (JToken)null;
					} else {
						this.NonNullInnerObject[settingName] = new JArray(defaultValue);
					}
				}
				return defaultValue;
			}
		}

		public IEnumerable<SettingsData> GetObjectArrayValue(string settingName, IEnumerable<SettingsData> defaultValue, bool createIfNotExist = false) {
			Value value = GetValue(settingName);
			if (value.IsNull == false) {
				return value.GetObjectArrayValue();
			} else {
				if (createIfNotExist) {
					if (defaultValue == null) {
						this.NonNullInnerObject[settingName] = (JToken)null;
					} else {
						JObject[] objectArray = defaultValue.Select(s => s.InnerObject).ToArray();
						this.NonNullInnerObject[settingName] = new JArray(objectArray);
					}
				}
				return defaultValue;
			}
		}

		#endregion


		#region methods - set value

		public bool RemoveValue(string settingName) {
			// argument checks
			if (settingName == null) {
				throw new ArgumentNullException(nameof(settingName));
			}

			return this.NonNullInnerObject.Remove(settingName);
		}

		public void SetValue(string settingName, Value value) {
			// argument checks
			if (settingName == null) {
				throw new ArgumentNullException(nameof(settingName));
			}

			this.NonNullInnerObject[settingName] = value.InnerObject;
		}

		public void SetStringValue(string settingName, string value, bool omitDefault = false, string defaultValue = null) {
			// argument checks
			if (settingName == null) {
				throw new ArgumentNullException(nameof(settingName));
			}

			// state checks
			EnsureHasStorage();
			JObject innerObject = this.InnerObject;

			// add a setting if necessary 
			if (omitDefault && string.Compare(value, defaultValue, StringComparison.Ordinal) == 0) {
				innerObject.Remove(settingName);
			} else {
				innerObject[settingName] = value;
			}

			return;
		}

		public void SetInt32Value(string settingName, int value, bool omitDefault = false, int defaultValue = 0) {
			// argument checks
			if (settingName == null) {
				throw new ArgumentNullException(nameof(settingName));
			}

			// state checks
			EnsureHasStorage();
			JObject innerObject = this.InnerObject;

			// add a setting if necessary 
			if (omitDefault && value == defaultValue) {
				innerObject.Remove(settingName);
			} else {
				innerObject[settingName] = value;
			}

			return;
		}

		public void SetNullableInt32Value(string settingName, int? value) {
			// argument checks
			if (settingName == null) {
				throw new ArgumentNullException(nameof(settingName));
			}

			// state checks
			EnsureHasStorage();
			JObject innerObject = this.InnerObject;

			// add a setting if necessary 
			if (value.HasValue) {
				innerObject[settingName] = value.Value;
			} else {
				innerObject.Remove(settingName);
			}

			return;
		}

		public void SetDoubleValue(string settingName, double value, bool omitDefault = false, double defaultValue = 0) {
			// argument checks
			if (settingName == null) {
				throw new ArgumentNullException(nameof(settingName));
			}

			// state checks
			EnsureHasStorage();
			JObject innerObject = this.InnerObject;

			// add a setting if necessary 
			if (omitDefault && value == defaultValue) {
				innerObject.Remove(settingName);
			} else {
				innerObject[settingName] = value;
			}

			return;
		}

		public void SetBooleanValue(string settingName, bool value, bool omitDefault = false, bool defaultValue = false) {
			// argument checks
			if (settingName == null) {
				throw new ArgumentNullException(nameof(settingName));
			}

			// state checks
			EnsureHasStorage();
			JObject innerObject = this.InnerObject;

			// add a setting if necessary 
			if (omitDefault && value == defaultValue) {
				innerObject.Remove(settingName);
			} else {
				innerObject[settingName] = value;
			}

			return;
		}

		public void SetObjectValue(string settingName, SettingsData value, bool omitIfNull = false) {
			// argument checks
			if (settingName == null) {
				throw new ArgumentNullException(nameof(settingName));
			}

			// state checks
			EnsureHasStorage();
			JObject innerObject = this.InnerObject;

			// add a setting
			if (omitIfNull && value.IsNull) {
				innerObject.Remove(settingName);
			} else {
				innerObject[settingName] = value.InnerObject;
			}

			return;
		}

		public void SetEnumValue(string settingName, Enum value, bool omitDefault = false, object defaultValue = null) {
			// argument checks
			if (settingName == null) {
				throw new ArgumentNullException(nameof(settingName));
			}

			// state checks
			EnsureHasStorage();
			JObject innerObject = this.InnerObject;

			// add a setting if necessary 
			if (omitDefault && object.Equals(value, defaultValue)) {
				innerObject.Remove(settingName);
			} else {
				innerObject[settingName] = value.ToString();
			}

			return;
		}

		public void SetDoubleArrayValue(string settingName, IEnumerable<double> value, bool omitIfNull = false) {
			// argument checks
			if (settingName == null) {
				throw new ArgumentNullException(nameof(settingName));
			}

			// state checks
			EnsureHasStorage();
			JObject innerObject = this.InnerObject;

			// add a setting
			if (omitIfNull && value == null) {
				innerObject.Remove(settingName);
			} else {
				if (value == null) {
					value = new double[0];
				}
				this.InnerObject[settingName] = new JArray(value);
			}

			return;
		}

		public void SetObjectArrayValue(string settingName, IEnumerable<SettingsData> value, bool omitIfNull = false) {
			// argument checks
			if (settingName == null) {
				throw new ArgumentNullException(nameof(settingName));
			}

			// state checks
			EnsureHasStorage();
			JObject innerObject = this.InnerObject;

			// add a setting
			if (omitIfNull && value == null) {
				innerObject.Remove(settingName);
			} else {
				if (value == null) {
					value = EmptySettingsArray;
				}
				JObject[] objectArray = value.Select(s => s.InnerObject).ToArray();
				this.InnerObject[settingName] = new JArray(objectArray);
			}

			return;
		}

		public void SetJsonValue(string settingName, string jsonText) {
			// argument checks
			if (settingName == null) {
				throw new ArgumentNullException(nameof(settingName));
			}
			if (string.IsNullOrEmpty(jsonText)) {
				throw new ArgumentNullException(nameof(jsonText));
			}

			JToken token = JToken.Parse(jsonText);
			this.NonNullInnerObject[settingName] = token;
		}

		#endregion


		#region methods - load & save

		public static SettingsData Load(string filePath, bool createIfNotExist) {
			// argument checks
			if (filePath == null) {
				throw new ArgumentNullException(nameof(filePath));
			}

			// load settings from the file
			JObject jsonObject = null;
			try {
				using (TextReader reader = File.OpenText(filePath)) {
					jsonObject = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
				}
			} catch (FileNotFoundException) {
				if (createIfNotExist == false) {
					throw;
				}
				// continue
			} catch (DirectoryNotFoundException) {
				if (createIfNotExist == false) {
					throw;
				}
				Directory.CreateDirectory(Path.GetDirectoryName(filePath));
				// continue
			}
			if (jsonObject == null && createIfNotExist) {
				// create empty one
				jsonObject = new JObject();
				using (TextWriter writer = File.CreateText(filePath)) {
					writer.Write(jsonObject.ToString());
				}
			}

			return new SettingsData(jsonObject);
		}

		public void Save(string filePath) {
			// argument checks
			if (filePath == null) {
				throw new ArgumentNullException(nameof(filePath));
			}

			// create folder if it does not exist
			string folderPath = Path.GetDirectoryName(filePath);
			if (Directory.Exists(folderPath) == false) {
				Directory.CreateDirectory(folderPath);
			}

			string tempFilePath = string.Concat(filePath, ".tmp");
			string backupFilePath = string.Concat(filePath, ".bak");

			// save settings to the temp file
			using (TextWriter writer = File.CreateText(tempFilePath)) {
				writer.Write(this.InnerObject.ToString());
			}

			// update the settings file and the backup file state
			File.Delete(backupFilePath);
			if (File.Exists(filePath)) {
				File.Move(filePath, backupFilePath);
			}
			File.Move(tempFilePath, filePath);

			return;
		}

		#endregion


		#region methods - misc

		public static bool AreSameSettingNames(string name1, string name2) {
			// setting names are case-sensitive and not localized
			return string.Compare(name1, name2, StringComparison.Ordinal) == 0;
		}

		public static SettingsData EmptySettingsGenerator() {
			return new SettingsData(new JObject());
		}

		public void EnsureHasStorage() {
			if (this.InnerObject == null) {
				throw new InvalidOperationException($"This operation cannot be called on the object which is specified no {nameof(InnerObject)}.");
			}
		}

		#endregion


		#region overrides

		public override string ToString() {
			return this.InnerObject?.ToString();
		}

		#endregion
	}
}
