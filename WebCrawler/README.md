﻿# 依頼１
  
.NET6でWebクローラーを作ってください。  
対象のページは、  
https://manga-zip.is/post/page/1  
～  
https://manga-zip.is/post/page/8859
までの8859ページです。  
ページ含まれるaタグでrel属性が「bookmark」のタグに含まれる文字を集めます。  
その文字をファイル「Title.txt」へ書き出してください。  



# 依頼２
.NET6でプログラムを作ってください。
「Title.txt」ファイルを一行づつ読みとり下記の処理を行ってください。
その処理とは、一行の文字列の[]の中にある文字をKeyとし、[]以外の文字をValueとします。  
  
Keyは、「スペース+vol」以降の文字をカットします。  
Keyには、「スペース+vol」の文字がない場合はそのまま値を使用します。  

Valueは、「スペース＋第」以降の文字をカットします。  
もしくは、「第01」や「第02」などの文字以降をかっとします。  
Valueには、「スペース＋第」の文字がない場合はそのまま値を使用します。  
  
KeyとValueは、カンマ区切りのテキストファイル「TitleKeyValue.txt」として出力してください。  
テキストファイルの出力順序は、Key順で出力してください。  

## 追加依頼
Keyが重複した場合は、上書き処理を入れてください。  
## 追加依頼
すべての行ではなく、処理が難しそうな部分に日本語のコメントを入れてください。  


# メモ
最大ページ数は、8895ページ

