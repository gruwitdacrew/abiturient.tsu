import React, { Component } from "react";
import './style/common.css';
import Login from "./Login";
import Documents from "./Documents";

class Profile extends Component {
  componentDidMount() {
    // Выполнение запроса при загрузке страницы
    fetch(window.server + "/api/users/profile", {
      method: 'GET',
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
      }
    })
    .then(response => {
      if (response.status === 401)
      {
        this.props.refresh(() => {this.componentDidMount()});
        return;
      }
      else if (response.status !== 500) return response.json();
    })
    .then(data => {
      if (!data) {
        return;
      }
      if (data.statusCode) {
        alert(data.message);
      }
      else{
        document.getElementById("email").value = data.email;
        document.getElementById("phone").value = data.phone;
        document.getElementById("fullName").value = data.fullName
        document.getElementById("birthDate").value = data.birthDate
        document.getElementById("gender").value = data.gender
        document.getElementById("nationality").value = data.nationality
        document.getElementById("role").value = data.roles.join(" | ");
      }
    });
  }

  logout = async() => {
    fetch(window.server + "/api/users/logout", {
      method: 'POST',
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
      }
    })
    .then(response => {
      if (response.status === 401)
      {
        this.props.refresh(() => {this.logout()});
        return;
      }
      else if (response.status === 200) return;
    })
    .then(data => {
      localStorage.removeItem('accessToken');
      localStorage.removeItem('refreshToken');

      this.props.setAuthenticated(false);
      this.setState({redirectToLogin: true})
    });
  }

  render() {

    if (this.state && this.state.redirectToLogin) {
      return <Login />;
  }

    if (this.state && this.state.redirectToDocuments) {
      return <Documents />;
  }

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

        <button type="submit" class="submit text" style={{backgroundColor: "red"}} onClick={this.logout}>Выйти</button>
        
      </div>
    );
  }
}

export default Profile;