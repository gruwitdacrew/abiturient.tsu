import React, { Component } from "react";
import './style/common.css';
import Login from "./Login";
import Documents from "./Documents";

class Profile extends Component {
  componentDidMount() {
    // Выполнение запроса при загрузке страницы
    fetch(window.users + "/api/users/profile", {
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

  edit = async() => {
    fetch(window.users + "/api/users/profile", {
      method: 'PATCH',
      body: JSON.stringify({  email: document.getElementById("email").value || undefined,
                              phone: document.getElementById("phone").value || undefined,
                              fullName: document.getElementById("fullName").value || undefined,
                              birthDate: document.getElementById("birthDate").value || undefined,
                              gender: document.getElementById("gender").value || undefined,
                              nationality: document.getElementById("nationality").value || undefined,
                           }),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
      }
    })
    .then(response => {
      if (response.status === 401)
      {
        this.props.refresh(() => {this.edit()});
        return;
      }
      else if (response.status !== 200) return response.json();
      else return;
    })
    .then(data => {
      if (!data) {
        return;
      }
      if (data.statusCode) {
        alert(data.message);
      }
    });
  }

  logout = async() => {
    fetch(window.users + "/api/users/logout", {
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
      else if (response.status !== 200) return response.json();
      else return;
    })
    .then(() => {

        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
  
        this.props.setAuthenticated(false);
        this.setState({redirectToLogin: true})
    });
  }

  changePassword = async() => {
    fetch(window.users + "/api/users/password", {
      method: 'PUT',
      body: JSON.stringify({ password: document.getElementById('passwordOld').value, newPassword: document.getElementById('passwordNew').value }),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
      }
    })
    .then(response => {
      if (response.status === 401)
      {
        this.props.refresh(() => {this.changePassword()});
        return;
      }
      else if (response.status !== 200) return response.json();
      else return;
    })
    .then(data => {
      if (!data) {
        return;
      }
      if (data.statusCode) {
        alert(data.message);
      }
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

        <button type="submit" class="submit text" style={{backgroundColor: "red"}} onClick={this.logout}>Выйти</button>

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

          
          <button type="submit" class="submit text" onClick={this.edit} >OK</button>
        </form>

        <form id="login-form" class = "form">
          <div id="alert"></div>

          <label class = "text" style={{fontSize: '50px', textAlign: "center"}}>Password Change</label>

          <div class="form-column">
            <label class = "text" for="password">Password</label>
            <input class = "text" type="password" id="passwordOld" />
          </div>

          <div class="form-column">
            <label class = "text" for="password">New Password</label>
            <input class = "text" type="password" id="passwordNew" />
          </div>
          
          <button type="submit" class="submit text" onClick={this.changePassword}>OK</button>
        </form>
        
      </div>
    );
  }
}

export default Profile;