# ExpressionTreeCalculator
## How To Run
### 1: Download and run the exe file
The exe file can be found in the repository or [click here](https://github.com/ChrisMansourian/ExpressionTreeCalculator/blob/main/ExpressionTreeCalculator.exe)
<br/>
When downloaded, windows might have a warning about security risks due to unknown publisher.  To get passed this, click More Info, then Run Anyway.
### 2: Download the code and run the program
To download and run the code, be sure to have an IDE such as Visual Studio that is capable of running C# code.
<br/>
## How To Use
When run, the program will prompt you to enter in an equation (this can have variables in it).  If there are any variables, it will ask for values to give the variables before it solves the equation.  You can then use the same equation again and plug in different numbers for the variables or enter a new equation.
<br/>
## How It Works
The inputted equation is converted to Reverse Polish Notation (RPN) and constructed into an Expression Tree which holds all the operators, variables and numbers used in the equation.  When the user wants to solve, all the variables need to be given a value.  Note, a variable can occur multiple times within the same equation.
<br/>
#### (5+a)/b +2/(a*c)
In the abovve example, the a occurs twice.  
<br/>
The program will convert this to the RPN expression: 5 a + b / 2 a c * / +
<br/>
The RPN can then be used to make the Expression Tree.
