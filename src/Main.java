import java.lang.reflect.InvocationTargetException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.ArrayList;

import static java.util.stream.Collectors.*;

public class Main {

    public static void main(String[] args) throws SQLException, NoSuchMethodException, NoSuchFieldException, InvocationTargetException, IllegalAccessException, InstantiationException, ClassNotFoundException {
        DemoFramework demoFramework = new DemoFramework(
                "jdbc:mysql://localhost:3306/registration?useSSL=false&createDatabaseIfNotExist=true&serverTimezone=UTC"
                ,"root"
                ,"");


        User2 user = new User2("hi148","jo168","777388");
          demoFramework.createTableByModel(user);
          demoFramework.add(user);





	}
}
