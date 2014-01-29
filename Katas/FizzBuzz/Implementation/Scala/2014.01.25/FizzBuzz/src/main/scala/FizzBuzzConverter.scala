object FizzBuzzConverter {
  
  def getConvertedNumbersUpTo(maxNumber: Int) : List[String] = {
    var result : List[String]= Nil
    for(currentNumber <- maxNumber to 1 by -1)
      result = convert(currentNumber) :: result
    return result
  }
  
  def convert(number: Int) : String = {
    val isFizzNumber = this.isFizzNumber(number);
    val isBuzzNumber = this.isBuzzNumber(number);
    (isFizzNumber, isBuzzNumber) match {
      case (true, true) => "FizzBuzz"
      case (true, _) => "Fizz"
      case (_, true) => "Buzz"
      case _ => number.toString
    }
  }
  
  private def isFizzNumber(number: Int) : Boolean = isDividableBy(number, 3) || containsDigit(number, 3);
  
  private def isBuzzNumber(number: Int) = isDividableBy(number, 5) || containsDigit(number, 5);
  
  private def isDividableBy(number: Int, denominator: Int) = number % denominator == 0;
  
  private def containsDigit(number: Int, digit: Int) : Boolean = {
    if(number % 10 == digit) 
      return true
    return number != 0 && containsDigit(number / 10, digit)
  }
}
