class Math
{
    public static List<string> MathScript = new List<string>(){
        "int sqrt(int value){",
        "for(int i = 0; i < value; i++){",
        "if(i * i == value){",
        "return i;",
        "}",
        "}",
        "}",
        "int pow(int base, int power){",
        "int product = base;",
        "for(int i = 1; i < power; i++){",
        "product = product * base;",
        "}",
        "return product;",
        "}",
    };
}