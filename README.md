# MobileTest

Clone it! Don't fork it or other people can see your implementation.

This is assignment is base on Xamarin
https://www.xamarin.com

Please refer the iOS project, there are bare bone implementation so you can verify your enviroment is workable for further development. 

This simple application has 2 screen, which are list screen and upload screen.

![alt tag](https://cloud.githubusercontent.com/assets/1186623/13046748/b4ad69a4-d415-11e5-9ba9-c7c30780427b.png)

The basic functionality you need to implement are:

For the list screen
- display collection of uploaded photo. 

For the upload screen,
- upload a photo through drawing api (You don't need to implement it. You can find it in Core Porject > Domain > UpdateItem). 
- The nature :smiling_imp: of this api will randomly block you serveral second so your UI should consider to embrace or avoid concurrency.
- pick a last taken photo from device library
 
![alt tag](https://cloud.githubusercontent.com/assets/1186623/13080516/f47235bc-d503-11e5-8b8f-a667ffe5dbff.png)

You will be evaluated on: 
- technical capability of the code
- code structure of how easy to write, debug and maintain
- UI presentation
- other design decisions

You are expceted to implement this user interface for iOS, android or window mobile. We recommend you to focus on your favourite platform but you gain a bouns if you can make it works for both.

Hints:The ultimate solution of this assignment is ReactiveUI
http://reactiveui.net/
