import org.scalatest.FunSuite
import org.junit.runner.RunWith
import org.scalatest.junit.JUnitRunner
import org.scalatest.Matchers

@RunWith(classOf[JUnitRunner])
class DoorStateGeneratorSuite extends FunSuite with Matchers {
  
  test("generate for 1 door") {
    DoorStateGenerator.generateFor(1) should equal(Array("open"))
  }
  
  test("generate for 2 doors") {
    DoorStateGenerator.generateFor(2) should equal(Array("open", "closed"))
  }
  
  test("generate for 3 doors") {
    DoorStateGenerator.generateFor(3) should equal(Array("open", "closed", "closed"))
  }
  
  test("generate for 10 doors") {
    DoorStateGenerator.generateFor(10) should equal(
        Array("open", "closed", "closed", "open", "closed", "closed", "closed", "closed", "open", "closed"))
  }
}
