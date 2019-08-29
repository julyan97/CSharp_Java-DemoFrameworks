class User2 {

      private String email;
      private String firstName;
      private String lastName;

      public  User2(){}
      
      public User2(String email, String firstName, String lastName) {
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
      }

      public String getEmail() {
            return email;
      }

      public void setEmail(String email) {
            this.email = email;
      }

      public String getFirstName() {
            return firstName;
      }

      public void setFirstName(String firstName) {
            this.firstName = firstName;
      }

      public String getLastName() {
            return lastName;
      }

      public void setLastName(String lastName) {
            this.lastName = lastName;
      }
}
