using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public interface IGetter {
    string GetName();
    object GetValue(object target);
}
public class GetterField : IGetter{
    FieldInfo f; 
    public GetterField(FieldInfo f) { this.f = f;}
    public string GetName() { return f.Name; }
    public object GetValue(object target) {
        return f.GetValue(target);
    }
}
public class GetterMethod : IGetter{
    MethodInfo m;
    public GetterMethod(MethodInfo m) {this.m = m;}
    public string GetName() { return m.Name; }
    public object GetValue(object target) { 
        return m.Invoke(target, new object[0]);
    }
}
public class GetterProperties : IGetter{
	PropertyInfo p;
	public GetterProperties(PropertyInfo p) {this.p = p;}
	public string GetName() { return p.Name; }
	public object GetValue(object target) {
		return p.GetValue(target);
	}
}
public class Logger {
	
	bool rf = false;
	bool rm = false;
	bool rp = false;

    public void Log(object o) {
        Type t = o.GetType();
        if(t.IsArray) LogArray((IEnumerable) o);
        else {
			List<IGetter> getters = new List<IGetter>();
			if(rm) getters = InitMethods(t);
			if(rf){
				var fs = InitFields(t);
				getters.AddRange(fs);
			}
			if(rp) {
				var gp = InitProperties(t);
				getters.AddRange(gp);
			}     
            // var fs = InitFields(t ); // 1x
            // var getters = InitMethods(t ); // 1x
			// var gp = InitProperties(t);
            // getters.AddRange(fs);
			// getters.AddRange(gp);
            LogObject(o, getters);
        }
    }
    
    public IEnumerable<IGetter> InitFields(Type t) {
        List<IGetter> l = new List<IGetter>();
        foreach(FieldInfo m in t.GetFields()) {
            l.Add(new GetterField(m));
        }
        return l;
    }
    public List<IGetter> InitMethods(Type t) {
        List<IGetter> l = new List<IGetter>();
        foreach(MethodInfo m in t.GetMethods()) {
            if(m.ReturnType != typeof(void) && m.GetParameters().Length == 0) {
                l.Add(new GetterMethod(m));
            }
        }
        return l;
    }
	public IEnumerable<IGetter> InitProperties(Type t) {
		List<IGetter> l = new List<IGetter>();
		foreach(PropertyInfo m in t.GetProperties())
			l.Add(new GetterProperties(m));
		return l;
	}
    
    public void LogArray(IEnumerable o) {
        Type elemType = o.GetType().GetElementType(); // Tipo dos elementos do Array
		List<IGetter> getters = new List<IGetter>();
		if(rm) getters = InitMethods(elemType);
		if(rf){
			var fs = InitFields(elemType);
			getters.AddRange(fs);
		}
		if(rp) {
			var gp = InitProperties(elemType);
			getters.AddRange(gp);
		}     
		Console.WriteLine("Array of " + elemType.Name + "[");
        foreach(object item in o) LogObject(item, getters); // * 
        Console.WriteLine("]");
    }
    
    public void LogObject(object o, IEnumerable<IGetter> gs) {
        Type t = o.GetType();
        Console.Write(t.Name + "{");
        foreach(IGetter g in gs) {
            Console.Write(g.GetName() + ": ");
            Console.Write(g.GetValue(o) + ", ");
        }
        Console.WriteLine("}");
    }
	public void ReadFields(){ rf = true;}
	public void ReadMethods(){rm = true;}
	public void ReadProperties(){ rp = true;}
}