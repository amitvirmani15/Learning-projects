using System;
using System.Collections.Generic;
using System.Linq.Expressions;

internal class Program
{
    private static void Main(string[] args)
    {
        Inventor tesla = new Inventor("Nikola Tesla", new DateTime(1856, 7, 9), "Serbian");
        var param = Expression.Parameter(typeof(Inventor));


        string tree = "maple";

        System.Reflection.MethodInfo addMethod = typeof(Dictionary<int, string>).GetMethod("Add");

        // Create an ElementInit that represents calling
        // Dictionary<int, string>.Add(tree.Length, tree).
        System.Linq.Expressions.ElementInit elementInit =
            System.Linq.Expressions.Expression.ElementInit(
                addMethod,
                System.Linq.Expressions.Expression.Constant(tree.Length),
                System.Linq.Expressions.Expression.Constant(tree));

        Console.WriteLine(Expression.New());

        //Expression.PropertyOrField(tesla, "Name");

        // BlockExpression();

        Console.WriteLine("Hello, World!");
    }

    private static void BlockExpression()
    {
        // Add the following directive to your file:
        // using System.Linq.Expressions;

        // The block expression allows for executing several expressions sequentually.
        // When the block expression is executed,
        // it returns the value of the last expression in the sequence.
        BlockExpression blockExpr = Expression.Block(
            Expression.Call(
                null,
                typeof(Console).GetMethod("Write", new Type[] { typeof(String) }),
                Expression.Constant("Hello ")
               ),
            Expression.Call(
                null,
                typeof(Console).GetMethod("WriteLine", new Type[] { typeof(String) }),
                Expression.Constant("World!")
                ),
            Expression.Constant(42)
        );

        Console.WriteLine("The result of executing the expression tree:");
        // The following statement first creates an expression tree,
        // then compiles it, and then executes it.
        var result = Expression.Lambda<Func<int>>(blockExpr).Compile()();

        // Print out the expressions from the block expression.
        Console.WriteLine("The expressions from the block expression:");
        foreach (var expr in blockExpr.Expressions)
            Console.WriteLine(expr.ToString());

        // Print out the result of the tree execution.
        Console.WriteLine("The return value of the block expression:");
        Console.WriteLine(result);

                       
        // This code example produces the following output:
        //
        // The result of executing the expression tree:
        // Hello World!

        // The expressions from the block expression:
        // Write("Hello ")
        // WriteLine("World!")
        // 42

        // The return value of the block expression:
        // 42
    }

    private static void BinaryExpression()
    {
        System.Linq.Expressions.BinaryExpression binaryExpression =
    System.Linq.Expressions.Expression.MakeBinary(
        System.Linq.Expressions.ExpressionType.Subtract,
        System.Linq.Expressions.Expression.Constant(53),
        System.Linq.Expressions.Expression.Constant(14));

        Console.WriteLine(binaryExpression.ToString());

        // Create an expression using expression lambda
        Expression<Func<int, int, int>> expression = (num1, num2) => num1 + num2;
        Console.WriteLine(((expression.Compile())(5, 7)));
    }

    private static void LambdaExpression()
    {
        ParameterExpression num1 = Expression.Parameter(typeof(int), "num1");
        ParameterExpression num2 = Expression.Parameter(typeof(int), "num2");
        //Create the expression parameters
        ParameterExpression[] parameters = new ParameterExpression[] { num1, num2 };

        BinaryExpression body = Expression.Multiply(num1, num2);


        //Create the expression 
        Expression<Func<int, int, int>> expression1 = Expression.Lambda<Func<int, int, int>>(body, parameters);
        Console.WriteLine(((expression1.Compile())(5, 7)));
    }
}