print("Hello world! YAY!")
import ConsoleApp1
import System.Linq.Enumerable
x = ConsoleApp1.SomeObject()
x.x="Yupii one!"
x.l.Add(ConsoleApp1.SomeObject())
print("x.l.count:" + str(x.l.Count))
x.l[0].x="Yupii one.one!"
x.l.Add(ConsoleApp1.SomeObject())
x.l[1].x="Yupii one.two!"
output = System.Linq.Enumerable.Take(x.l,2)
print("output=" + str(output))
input.x=input.x + " it works my preciousssss!!!!" 

