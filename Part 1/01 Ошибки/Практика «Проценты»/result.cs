public static double Calculate(string userInput)
{
	string[] data = userInput.Split(new char[] { ' ' });
    //string data0 = data[0].Replace('.', ',');
    //string data1 = data[1].Replace('.', ',');
    double depositStart = Convert.ToDouble(data[0]);
    double value = Convert.ToDouble(data[1]);
    double monthCount = Convert.ToDouble(data[2]);
    double depositEnd = depositStart * (Math.Pow((1.0 + (value / (12.0 * 100.0))), monthCount));
    return depositEnd;
}