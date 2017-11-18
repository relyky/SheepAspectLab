# SheepAspectLab
AOP模組 SheepAspect 練習

## 參考
[CodePlex-SheepAspect(已過時)](https://sheepaspect.codeplex.com/)  
[nuget-SheepAspect](https://www.nuget.org/packages/SheepAspect)  
[Aspect-Oriented Programming for .NET(改由此維護)](https://github.com/erhan0/aop)  
[.NET中AOP方便之神SheepAspect](http://www.itread01.com/articles/1476766551.html)  
[SheepAOP Part 1 – Getting Started](https://hendryluk.wordpress.com/2011/05/08/sheepaop-part-1-getting-started/)  
[SheepAOP Part 2 – Pointcut and ](https://hendryluk.wordpress.com/2011/05/09/sheepaop-part-2-pointcut-and-saql-basics/)  

## 一些開發說明
雖 SheepAspect 很有彈性，因是向 AspectJ 取經的關係。   
但應用原則上： 1 Advice(Aspect instance) ←→ 1 Pointcut ←→ 1 Attribute ←→ N 纏繞目標   
等同 => 1 Aspect ←→ N 纏繞目標   
利用 Attribute 纏繞目標即可，讓“纏繞”精簡化才符合 AOP 的初心。   
