object DoorStateGenerator {
  def generateFor(numberOfDoors: Int) : Seq[String] = {
    for(doorNumber <- 1 to numberOfDoors;
        finalDoorState = calculateFinalDoorStateFor(doorNumber))
      yield finalDoorState;
  }
  
  private def calculateFinalDoorStateFor(doorNumber: Int) : String = {
    var currentState = false
    for(i <- 1 to doorNumber if ((doorNumber % i) == 0))
      currentState = !currentState
    currentState match { case false => "closed" case true => "open"}
  }
}
