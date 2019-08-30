import Data.DemoFramework;

import java.lang.reflect.InvocationTargetException;
import java.sql.SQLException;
import java.util.List;

public class Main {

    public static void main(String[] args) throws SQLException, NoSuchMethodException, NoSuchFieldException, InvocationTargetException, IllegalAccessException, InstantiationException, ClassNotFoundException {
        DemoFramework db = new DemoFramework(
                "jdbc:mysql://localhost:3306/registration?useSSL=false&createDatabaseIfNotExist=true&serverTimezone=UTC"
                ,"root"
                ,"");

                  Petkan p=new Petkan(25,true,3.9F,"mazda");

                  db.createTableByModel(Petkan.class);
                  //db.addByModel(p);
        //Todo MAKE IT WORK
        //  List select = db.select("*", Petkan.class);

        // db.addByModel(p);
        //db.deleteByModel(p);
         // db.delete(Petkan.class.getSimpleName(),"razmer = '2'");
         // db.pureQuery("Delete from petkans");
         // db.update(Petkan.class.getSimpleName()+"s","");
    }
}
