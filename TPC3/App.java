import java.io.File;
import java.lang.reflect.Constructor;
import java.lang.reflect.Field;
import java.lang.reflect.Method;

class A {}
class B extends A {}
class C extends B {
    public C(){}
    public static int x, y;
    public static void Foo() {}
}

public class App {

    public static void main(String[] args) {
        PrintBaseTypes("Ola");
        PrintBaseTypes(19);
//        PrintBaseTypes(new C());
        PrintBaseTypes(new File("."));
        PrintMembers(new C());
        PrintMethods(new C());
        PrintFields(new C());
    }

    public static void PrintMembers(Object obj) {
        System.out.print("Members: ");
        for(Method methods : obj.getClass().getMethods())System.out.print(methods.getName() + " ");
        for(Constructor constructor : obj.getClass().getConstructors()) System.out.print(constructor.getName() + " ");
        for(Field fields : obj.getClass().getFields()) System.out.print(fields.getName() + " ");
        System.out.println();
    }
    public static void PrintMethods(Object obj) {
       System.out.print("Methods: ");
        for(var m : obj.getClass().getMethods()) System.out.print(m.getName() + " ");
        System.out.println();
    }

    public static void PrintFields(Object obj) {
       System.out.print("Fields: ");
        for(var m : obj.getClass().getFields()) System.out.print(m.getName() + " ");
        System.out.println();
    }
//
//    // Não Fazer => Avaliação em Tempo de Execução
//    // static readonly Type typeOfObject = (new Object()).GetType();
//
    public static void PrintBaseTypes(Object obj)
    {
        Class c = obj.getClass();
        do {
            System.out.print(c.getName() + " ");
            PrintInterfaces(c);
          c = c.getSuperclass();
        } while( c != Object.class);
        System.out.println();
    }
    public static void PrintInterfaces(Class t) {
        System.out.print("(");
        for (Class m : t.getInterfaces()) { System.out.print(m.getName() + " ");}
        System.out.print(")");
    }

}
