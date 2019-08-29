import javafx.beans.binding.StringBinding;

import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.ArrayList;

import static java.util.stream.Collectors.*;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.Arrays;

public class DemoFramework<T> implements Inter<T> {
      private Connection connection;

      public Connection getConnection() {
            return connection;
      }

      public DemoFramework(Connection connection) {
            this.connection = connection;
      }

      public void setConnection(String url, String name, String password) throws SQLException {
            this.connection = DriverManager.getConnection(url,name,password);
      }


      public DemoFramework(String url, String name, String password) throws SQLException {
            this.connection = DriverManager.getConnection(url,name,password);
      }

      @Override
      public void addByModel(T model12) throws SQLException, IllegalAccessException, InvocationTargetException {
            var declaredFields = model12.getClass().getDeclaredFields();
            var methods=model12.getClass().getDeclaredMethods();
            StringBuilder sb=new StringBuilder();
            StringBuilder sb2=new StringBuilder();
            //SB1
            sb.append(" (");
            for (int i = 0; i < declaredFields.length - 1; i++) {
                  sb.append(declaredFields[i].getName()).append(" , ");
            }
            sb.append(declaredFields[declaredFields.length-1].getName()).append(") ");

            //SB2

            var listMethods= Arrays.stream(methods)
                    .filter(x->x.getName().startsWith("get"))
                    .map(x-> {
                          try {
                                return x.invoke(model12);
                          } catch (IllegalAccessException e) {
                                e.printStackTrace();
                          } catch (InvocationTargetException e) {
                                e.printStackTrace();
                          }
                          return "";
                    })
                    .collect(toList());



            sb2.append(" (");
            for (int i = 0; i < listMethods.size()-1; i++) {
                  sb2.append("\"").append(listMethods.get(i)).append("\"").append(",");
            }
            sb2.append("\"").append(listMethods.get(listMethods.size() - 1)).append("\"").append(")");



            String query = "INSERT INTO "+model12.getClass().getSimpleName()+"s"+" "+sb.toString()+ "VALUES "+sb2.toString();
            System.out.println(query);
            connection.prepareStatement(query)
                    .execute();

      }

      @Override
      public void delete(String FromTable,String where) throws SQLException {
            String query = String.format(
                    "Delete %s where %s",
                    FromTable,
                    where
            );
            connection.prepareStatement(query)
                    .execute();
      }

      @Override
      public void deleteByModel(T mpdel) {

      }

      @Override
      public void update(String ToUpdate,String data) throws SQLException {
            String query = String.format(
                    "UPDATE `users` SET `%s`=%s",
                    ToUpdate,
                    data
            );
            connection.prepareStatement(query)
                    .execute();
      }

      @Override
      public void select(String select,String from) throws SQLException {
            var query=String.format("Select %s from %s"
                    ,select
                    ,from
                    );
            connection.prepareStatement(query);
      }

      @Override
      public void pureQuery(String query) throws SQLException {
            connection.prepareStatement(query).execute();
      }

      @Override
      public void createTableByModel(T model) throws SQLException {
            var fields=model.getClass().getDeclaredFields();
            StringBuilder sb=new StringBuilder();
            sb.append("Create table if not exists ")
                    .append(model.getClass().getName())
                    .append("s")
                    .append(" ( ")
                    .append("id int(11) not null auto_increment,");
            for (int i = 0; i < fields.length; i++) {
                  sb.append(" ")
                          .append(fields[i].getName())
                          .append(" varchar(100) not null, ");
            }
            sb.append("Primary key ")
                    .append("(")
                    .append("id")
                    .append(")")
                    .append(" );");
           connection.prepareStatement(sb.toString()).execute();
      }
}
