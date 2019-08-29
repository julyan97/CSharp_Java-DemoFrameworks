import java.lang.reflect.InvocationTargetException;
import java.sql.Connection;
import java.sql.SQLException;

public interface Inter<T> {

      void add(T object) throws SQLException, NoSuchMethodException, InvocationTargetException, IllegalAccessException, ClassNotFoundException, InstantiationException;

      void delete(String where) throws SQLException;

       void update(String ToUpdate, String data) throws SQLException;

       void select(String select, String from) throws SQLException;

       void pureQuery(String query) throws SQLException;

       void createTableByModel(T model) throws SQLException;
}
