[目次](Index.md)

---

# 注意事項

## 稼働中はインターネット設定を変更しないでください

「認証プロキシ爆発しろ！」は稼働中Windowsのインターネット設定を書き換え、
停止時に設定を戻します。
そのため、「認証プロキシ爆発しろ！」稼働中にインターネット設定を変更すると、
停止の際の設定の復元によって、
変更した内容が書き変わってしまう可能性があります。

## Digest認証には対応していません

「認証プロキシ爆発しろ！」が対応しているプロキシの認証方法はBasic認証のみです。
認証プロキシでDigest認証を要求する運用になっているものってあまり無いと思うんだけど、
どうかな？

## HTTP/2には対応していません

HTTP/1.0とHTTP/1.1のみに対応しています。
（完全に対応できているかは怪しいけど）