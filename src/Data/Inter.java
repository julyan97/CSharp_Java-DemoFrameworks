package Data;

import java.lang.reflect.InvocationTargetException;
import java.sql.Connection;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public interface Inter<T> {

      void addByModel(T object) throws SQLException, NoSuchMethodException, InvocationTargetException, IllegalAccessException, ClassNotFoundException, InstantiationException;

      void delete(String FromTable,String where) throws SQLException;

      void deleteByModel(T model) throws IllegalAccessException, SQLException;

       void update(String ToUpdate, String data) throws SQLException;

       void update(T model) throws SQLException;

       ArrayList<ArrayList<String>> select(String select, Class model) throws SQLException;

       void pureQuery(String query) throws SQLException;

       void createTableByModel(Class model) throws SQLException;


}
