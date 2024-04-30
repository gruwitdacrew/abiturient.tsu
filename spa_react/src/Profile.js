import React, { Component } from "react";

class Profile extends Component {
  render() {
    return (
      <div class = "container">

        <form id="signup-form" class = "form">
          <div id="alert"></div>

          <label class = "text" style={{fontSize: '50px', textAlign: "center"}}>Profile</label>



          <div class="form-column">
            <div class="form-row">
              <label class = "text" for="email">Почта</label>
              <input class = "text" type="email" id="email" />
            </div>

            <div class="form-row">
              <label class = "text" for="phone">Телефон</label>
              <input class = "text" type="phone" id="phone" />
            </div>

            <div class="form-row">
              <label class = "text" for="fullName">Полное Имя</label>
              <input class = "text" id="fullName" />
            </div>

            <div class="form-row">
              <label class = "text" for="birthDate">Дата рождения</label>
              <input class = "text" id="birthDate" />
            </div>

            <div class="form-row">
              <label class = "text" for="gender">Пол</label>
              <input class = "text" id="gender" />
            </div>

            <div class="form-row">
              <label class = "text" for="nationality">Национальность</label>
              <div style={{minWidth:"150px"}}></div>
              <input class = "text" id="nationality" />
            </div>

            <div class="form-row">
              <label class = "text" for="role">Роль</label>
              <input class = "text" id="role" disabled />
            </div>
          </div>

          
          <button type="submit" class="submit text">OK</button>
        </form>
        
      </div>
    );
  }
}

export default Profile;