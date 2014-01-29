import org.scalatest.FunSuite
import org.junit.runner.RunWith
import org.scalatest.junit.JUnitRunner
import org.scalatest.Matchers

@RunWith(classOf[JUnitRunner])
class FizzBuzzConverterSuite extends FunSuite with Matchers {
  
  test("convert normal number") {
    FizzBuzzConverter.convert(1) should equal("1")
  }
  
  test("convert Fizz number dividable by 3") {
    FizzBuzzConverter.convert(3) should equal("Fizz")
  }
  
  test("convert Buzz number dividable by 5") {
    FizzBuzzConverter.convert(5) should equal("Buzz")
  }
  
  test("convert FizzBuzz number dividable by 3 and 5") {
    FizzBuzzConverter.convert(15) should equal("FizzBuzz")
  }
  
  test("convert Fizz number containing 3") {
    FizzBuzzConverter.convert(13) should equal("Fizz")
  }
  
  test("convert Fizz number containing 5") {
    FizzBuzzConverter.convert(52) should equal("Buzz")
  }
  
  test("convert FizzBuzz number containing 3 and 5") {
    FizzBuzzConverter.convert(53) should equal("FizzBuzz")
  }
  
  test("get converted for up to maximum") {
    FizzBuzzConverter.getConvertedNumbersUpTo(3) should equal(List("1", "2", "Fizz"))
  }
}
