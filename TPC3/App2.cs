using System;

public class App2{
	static bool Foo(String msg){
		if(msg.Length == 1) return true;
		if(msg[0] != msg[msg.Length - 1]) return false;
		if(msg.Length == 2) return true;
		return Foo(msg.Substring(1,msg.Length - 2));
	}
	
	public static void Main(){
		Console.Write(Foo("Hello World"));
	}
}