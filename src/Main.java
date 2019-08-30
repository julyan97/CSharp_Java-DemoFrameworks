import java.lang.reflect.InvocationTargetException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.ArrayList;

import static java.util.stream.Collectors.*;

public class Main {

    public static void main(String[] args) throws SQLException, NoSuchMethodException, NoSuchFieldException, InvocationTargetException, IllegalAccessException, InstantiationException, ClassNotFoundException {
        DemoFramework db = new DemoFramework(
                "jdbc:mysql://localhost:3306/registration?useSSL=false&createDatabaseIfNotExist=true&serverTimezone=UTC"
                ,"root"
                ,"");

//TODO delete and update
        User2 user = new User2("hi1489","jo168","777388");
          // db.createTableByModel(User2.class);
          // db.addByModel(user);
           db.deleteByModel(user);





	}
}
