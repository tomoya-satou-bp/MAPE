[目次](Index.md)

---

# よくある質問

「よくある質問」といいつつ、実際は自分向けの覚え書きです。


### Q: これはFidllerみたいなものですか？

まあ、単機能のFidllerみたいなものかと言えば、そうかも。

認証プロキシを通すためにFiddlerを使ってみたりもしましたが、
以下の理由から別にツールを作りました。

* スクリプトをいじる必要（しかもちょっと複雑）があり、
非開発者な人に「これを使えばなんとかできます」とか言いにくい。
* ~~自動構成スクリプトでプロキシが設定されている環境では、
Fiddlerのプロキシ設定書き換えは「次で始まるアドレスにはプロキシを使用しない」を設定してくれない。
認証プロキシが設置されているような環境では自動構成スクリプトを利用していることが多いでしょうから、
これは面倒の元になりがちです。~~ どうやら設定を行うことができるらしい。

それはそれとして、
Fiddlerはデバッグツールとして便利に利用させていただいております。


### Q: Fidller Core使っているの？

使っていません。

Fiddler Coreは個人利用だと無料だけど、
社内利用や商用利用は有償になります。
ライセンス条件見てしばらく考えましたが、
「認証プロキシをちゃんと通す」というシーンを考えると
業務利用扱いになっちゃいそうなので、
httpを処理する部分も作りました。


### Q: 初期設定（/Save /ProxyOverride）が面倒なんだけど、自動化できない？

version 1.0.17.0以降不要になりました。頑張った。
結局裏でWinHttp APIの自動構成スクリプトによるプロキシ解決をそのまま利用するようにしました。
詳細は、ブログ『[「認証プロキシ爆発しろ！」が自動構成スクリプトに対応しました](http://ipponshimeji.cocolog-nifty.com/blog/2017/08/post-86b8.html)』を参照してください。

~~ごもっともですが、ちょっと実装が面倒。~~

~~問題は自動構成スクリプト（.pac）が使われている場合です。
認証プロキシが導入されているような環境ではほとんど使われているんじゃないかな。
この場合、「次で始まるアドレスにはプロキシを使用しない」に相当する判定を行うには、
Javascriptエンジンをロードして.pacファイルを実行させてみる必要があります。~~

~~できなくはないはずだけど、
というか.NET Frameworkは内部的にやっているっぽいので、
その処理を再利用できるようにしてくれれば可能なんだけど、
そこ頑張るほどじゃないかなあ、と。~~


### Q: Edgeでプロキシエラーが出る場合があるんだけど

はい。「認証プロキシ爆発しろ！」中継下で、
IEやChromeだと大丈夫なのに、
Edgeだけ「プロキシサーバーに接続できません」エラーが出る場合があります。
気づいているのは以下の場合。

* Edgeで新しいタブを開いた際に出てくる「新しいタブ」ページの「マイ フィード」の項
* プロキシの内側にあるページ（外部ページは問題ない）

理由はよくわかりません。
そもそも「認証プロキシ爆発しろ！」には接続に来ていないので、
Edgeが内部でプロキシの要不要をどう判定しているのかが謎。


### Q: 名前はMAPLEとかにした方が響きがよくない？

まあそうかもしれませんが、MAPLEって名前の著名なソフトウェアは既にあるんですよ。


### Q: あのアイコン何？

ソウルジェムのイメージです。

娘に発注して作ってもらいました。
「認証プロキシで設定に苦労しているとソウルジェムが濁るねん」とか言っていたところ、
ちょうど年末あたりからAmazon Primeでまどマギが見れるようになったので、
正月家族で見てました。

で、せっかくだから「ぴかー」と「どんより」なソウルジェムのアイコン作って、と娘に発注しました。
一番お絵かきツール慣れていそうなので。
でも、丸写しはまずいので、インスパイアされた感じで。
レギュラー陣のカラーは避けて、緑とかいいんじゃなかろうか。
「飾りとかどうする？」「んー、つるんとした卵型電球みたいなのでええで」

で、こんなんになりました。

現在、「もっとフラットデザインっぽくしてくれ」と改善要望中。


### Q: VS 2017ベースにしないの？

.NET Core 2が完成したらVS 2017に移行して、
コア部を.NET Coreベースにするつもり。
だけど、.NET CoreベースのC#プロジェクトって既存のC#プロジェクトと結構違っていて、
ちょこちょこ自動化している処理とかどうすっかねえ。
GUI部分は従来通りデスクトップ.NET Frameworkになるし、
両タイプをうまくまとめて取り扱えるかなあ。 
