print("Hello world! YAY!")
import ConsoleApp1
import ConsoleApp1.SomeObject as SomeObject
import System.Linq.Enumerable as Enumerable
from System.Collections.Generic import IEnumerable, List
import System.Func as Func
import traceback
def doFilter(item):
	try:
		return item.x.StartsWith("a")
	except:
		print(traceback.format_exc())
		raise
def doApply(list):
	try:
		return Enumerable.Where(list,doFilter)
	except:
		print(traceback.format_exc())
		raise
