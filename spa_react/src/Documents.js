import React, { Component } from "react";

class Documents extends Component {
  componentDidMount() {
    // Выполнение запроса при загрузке страницы
    this.getEducationData();
    this.getPassportDocument();
  }

  getEducationData = async () => {
    await this.getEducationDocumentTypes();
    await this.getEducationDocument();
  }

  getEducationDocument = async() =>
  {
    return fetch(window.documents + "/api/document/education", {
      method: 'GET',
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
      }
    })
    .then(response => {
      if (response.status === 401)
      {
        this.props.refresh(() => {this.getEducationDocument()});
        return;
      }
      if (response.status === 404)
      {
        document.getElementById('createEducation').style.display = "block";
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
        document.getElementById('inputsEducation').style.display = "block";
        if (data.scan) document.getElementById('scan_edu_exist').style.display = "block"; else document.getElementById('scan_edu').style.display = "block";
        document.getElementById('documentType').value = data.documentType;
        document.getElementById('number_edu').value = data.number;
        document.getElementById('date_edu').value = data.date;
        document.getElementById('grade').value = data.grade;
      }
      return;
    });
  }

  getEducationDocumentTypes = async() =>
  {
    if (document.getElementById('documentType') && document.getElementById('documentType').options)
    {
      return fetch(window.documents + "/api/document/education/types", {
        method: 'GET',
        headers: {
          'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
        }
      })
      .then(response => {
        if (response.status === 401)
        {
          this.props.refresh(() => {this.getEducationDocumentTypes()});
          return;
        }
        if (response.status === 404)
        {
          document.getElementById('createEducation').style.display = "block";
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
          const selectElement = document.getElementById('documentType');
  
          const optionElement = document.createElement('option');
          optionElement.value = "";
          optionElement.textContent = "";
          selectElement.appendChild(optionElement);
          data.forEach(option => {
            const optionElement = document.createElement('option');
            optionElement.value = option;
            optionElement.textContent = option;
            selectElement.appendChild(optionElement);
          });
        }
        return;
      });
    }
  }

  deleteEducation = async() =>
  {
    document.getElementById('inputsEducation').style.display = 'none';
    document.getElementById('createEducation').style.display = 'block';
    if (!(this.state && this.state.firstEducation))
    {
      fetch(window.documents + "/api/document/education", {
        method: 'DELETE',
        headers: {
          'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
        }
      })
      .then(response => {
        if (response.status === 401)
        {
          this.props.refresh(() => {this.deleteEducation()});
          return;
        }
        else if (response.status === 200)
        {
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
      });
  }
}

  createOrEditEducation = async() =>
  {
    if (this.state && this.state.firstEducation)
    {
      fetch(window.documents + "/api/document/education", {
        method: 'POST',
        body: JSON.stringify({  documentType: document.getElementById('documentType').value,
                                number: document.getElementById('number_edu').value, 
                                date: document.getElementById('date_edu').value, 
                                grade: document.getElementById('grade').value, 
                              }),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
        }
      })
      .then(response => {
        if (response.status === 401)
        {
          this.props.refresh(() => {this.createOrEditEducation()});
          return;
        }
        else if (response.status === 200)
        {
  
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
      });
    }
    else{
      fetch(window.documents + "/api/document/education", {
        method: 'PATCH',
        body: JSON.stringify({  documentType: document.getElementById('documentType').value,
                                number: document.getElementById('number_edu').value, 
                                date: document.getElementById('date_edu').value, 
                                grade: document.getElementById('grade').value, 
                              }),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
        }
      })
      .then(response => {
        if (response.status === 401)
        {
          this.props.refresh(() => {this.createOrEditEducation()});
          return;
        }
        else if (response.status === 200)
        {
  
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
      });
    }
  }
  

  createEducationButton = async() =>
  {
    document.getElementById('inputsEducation').style.display = 'block';
    document.getElementById('scan_edu').style.display = 'block';
    document.getElementById('createEducation').style.display = 'none';
    this.setState({ firstEducation: true });
  }
/////////////////////////////////////////////////////////////////////////////////////

  getPassportDocument = async() =>
  {
    return fetch(window.documents + "/api/document/passport", {
      method: 'GET',
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
      }
    })
    .then(response => {
      if (response.status === 401)
      {
        this.props.refresh(() => {this.getPassportDocument()});
        return;
      }
      if (response.status === 404)
      {
        document.getElementById('createPassport').style.display = "block";
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
        
        document.getElementById('inputsPassport').style.display = "block";
        if (data.scan) document.getElementById('scan_pas_exist').style.display = "block"; else document.getElementById('scan_pas').style.display = "block";
        document.getElementById('number_pas').value = data.number;
        document.getElementById('date_pas').value = data.date;
        document.getElementById('series').value = data.series;
      }
      return;
    });
  }

  deletePassport = async() =>
  {
    document.getElementById('inputsPassport').style.display = 'none';
    document.getElementById('createPassport').style.display = 'block';
    if (!(this.state && this.state.firstPassport))
    {
      fetch(window.documents + "/api/document/passport", {
        method: 'DELETE',
        headers: {
          'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
        }
      })
      .then(response => {
        if (response.status === 401)
        {
          this.props.refresh(() => {this.deletePassport()});
          return;
        }
        else if (response.status === 200)
        {
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
      });
  }
}

  createOrEditPassport = async() =>
  {
    if (this.state && this.state.firstPassport)
    {
      fetch(window.documents + "/api/document/passport", {
        method: 'POST',
        body: JSON.stringify({ 
                                series: document.getElementById('series').value, 
                                number: document.getElementById('number_pas').value, 
                                date: document.getElementById('date_pas').value, 
                              }),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
        }
      })
      .then(response => {
        if (response.status === 401)
        {
          this.props.refresh(() => {this.createOrEditPassport()});
          return;
        }
        else if (response.status === 200)
        {
  
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
      });
    }
    else{
      fetch(window.documents + "/api/document/passport", {
        method: 'PATCH',
        body: JSON.stringify({ 
          series: document.getElementById('series').value, 
          number: document.getElementById('number_pas').value, 
          date: document.getElementById('date_pas').value, 
        }),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
        }
      })
      .then(response => {
        if (response.status === 401)
        {
          this.props.refresh(() => {this.createOrEditPassport()});
          return;
        }
        else if (response.status === 200)
        {
  
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
      });
      this.uploadPassportScan();

    }
  }
  

  createPassportButton = async() =>
  {
    document.getElementById('inputsPassport').style.display = 'block';
    document.getElementById('scan_pas').style.display = 'block';
    document.getElementById('createPassport').style.display = 'none';
    this.setState({ firstPassport: true });
  }

  downloadEducationScan = async() =>
  {
    return fetch(window.documents + "/api/document/education/scan", {
      method: 'GET',
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
      }
    })
    .then(response => {
      if (response.status === 401)
      {
        this.props.refresh(() => {this.downloadEducationScan()});
        return;
      }
      if (response.status === 404)
      {
        return;
      }
      else if (response.status === 200) return response.blob();
      else return response.json();
    })
    .then(data => {
      if (!data)
      {
        return;
      }
      if (data.statusCode) {
        alert(data.message);
      }
      else{
        const url = window.URL.createObjectURL(data);
        const link = document.createElement('a');
        link.href = url;
        link.setAttribute('download', 'passport.pdf'); // Указываем имя файла для скачивания
        document.body.appendChild(link);
        link.click();
      }
      return;
    });
  }

  deleteEducationScan = async() =>
  {
    return fetch(window.documents + "/api/document/education/scan", {
      method: 'DELETE',
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
      }
    })
    .then(response => {
      if (response.status === 401)
      {
        this.props.refresh(() => {this.deleteEducationScan()});
        return;
      }
      else if (response.status !== 500) return response.json();
    })
    .then(data => {
      if (!data)
      {
        document.getElementById('scan_edu_exist').style.display = "none";
        document.getElementById('scan_edu').style.display = "block";
        return;
      }
      if (data.statusCode) {
        alert(data.message);
      }
      return;
    });
  }

  downloadPassportScan = async() =>
  {
    return fetch(window.documents + "/api/document/passport/scan", {
      method: 'GET',
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
      }
    })
    .then(response => {
      if (response.status === 401)
      {
        this.props.refresh(() => {this.downloadPassportScan()});
        return;
      }
      if (response.status === 404)
      {
        return;
      }
      else if (response.status === 200) return response.blob();
      else return response.json();
    })
    .then(data => {
      if (!data)
      {
        return;
      }
      if (data.statusCode) {
        alert(data.message);
      }
      else{
        const url = window.URL.createObjectURL(data);
        const link = document.createElement('a');
        link.href = url;
        link.setAttribute('download', 'passport.pdf'); // Указываем имя файла для скачивания
        document.body.appendChild(link);
        link.click();
      }
      return;
    });
  }

  deletePassportScan = async() =>
  {
    return fetch(window.documents + "/api/document/passport/scan", {
      method: 'DELETE',
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
      }
    })
    .then(response => {
      if (response.status === 401)
      {
        this.props.refresh(() => {this.deletePassportScan()});
        return;
      }
      else if (response.status !== 500) return response.json();
    })
    .then(data => {
      if (!data)
      {
        document.getElementById('scan_pas_exist').style.display = "none";
        document.getElementById('scan_pas').style.display = "block";
        return;
      }
      if (data.statusCode) {
        alert(data.message);
      }
      return;
    });
  }

  uploadEducationScan = async() =>
  {
    return fetch(window.documents + "/api/document/education/scan", {
      method: 'POST',
      body: new FormData().append('file', document.getElementById('scan_edu').files[0]),
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
      }
    })
    .then(response => {
      if (response.status === 401)
      {
        this.props.refresh(() => {this.uploadEducationScan()});
        return;
      }
      else if (response.status !== 500) return response.json();
    })
    .then(data => {
      if (!data)
      {
        return;
      }
      if (data.statusCode) {
        alert(data.message);
      }
      return;
    });
  }

  uploadPassportScan = async() =>
  {
    const formData = new FormData();
    formData.append('file', document.getElementById('scan_pas').files[0], document.getElementById('scan_pas').files[0].name); // Указываем 'file' в качестве имени поля

    return fetch(window.documents + "/api/document/passport/scan", {
      method: 'POST',
      body: formData,
      headers: {
        'Content-Type': 'multipart/form-data',
        'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
      }
    })
    .then(response => {
      if (response.status === 401)
      {
        this.props.refresh(() => {this.uploadPassportScan()});
        return;
      }
      else if (response.status !== 500) return response.json();
    })
    .then(data => {
      if (!data)
      {
        return;
      }
      if (data.statusCode) {
        alert(data.message);
      }
      return;
    });
  }

  render() {
    return (
      <div class = "container">

        <div class="form-row">
          <form class = "form">
            <div id="alert"></div>
            <label class = "text" style={{fontSize: '50px', textAlign: "center"}}>Документ об образовании</label>
            <div id="inputsEducation" style={{display: "none"}}>

              <div class="form-column">
                <label class = "text" for="documentType">Тип документа</label>
                <select class = "text" id="documentType">
                </select>
              </div>

              <div class="form-column">
                <label class = "text" for="number_edu">Номер документа</label>
                <input class = "text" id="number_edu" />
              </div>

              <div class="form-column">
                <label class = "text" for="date_edu">Дата выдачи</label>
                <input class = "text" id="date_edu" />
              </div>

              <div class="form-column">
                <label class = "text" for="grade">Оценка</label>
                <input class = "text" id="grade" />
              </div>

              <div class="form-column">
                <label class = "text" for="scan_edu">Скан</label>
                <input type="file" class = "text" id="scan_edu" style={{display: "none"}} />
                <div class="form-row" id ="scan_edu_exist" style={{display: "none"}}>
                  <button type="submit" class="submit text" onClick={this.downloadEducationScan} style={{display: "none"}} >Скачать</button>
                  <button type="submit" class="submit text" style={{backgroundColor:"red"}} onClick={this.deleteEducationScan}>Удалить</button>
                </div>
              </div>
              
              <div class="form-row">
                <button type="submit" class="submit text" onClick={this.createOrEditEducation}>OK</button>
                <button type="submit" class="submit text" style={{backgroundColor:"red"}} onClick={this.deleteEducation}>Удалить</button>
              </div>

            </div>

            <button id="createEducation" type="submit" class="submit text" style={{backgroundColor:"blue", display:"none"}} onClick={this.createEducationButton} >Создать</button>
          </form>

          <form class = "form">
            <div id="alert"></div>
            <label class = "text" style={{fontSize: '50px', textAlign: "center"}}>Паспорт</label>
            <div id="inputsPassport" style={{display: "none"}}>

              <div class="form-column">
                <label class = "text" for="series">Серия</label>
                <input class = "text" id="series" />
              </div>

              <div class="form-column">
                <label class = "text" for="number_pas">Номер</label>
                <input class = "text" id="number_pas" />
              </div>

              <div class="form-column">
                <label class = "text" for="date_pas">Дата выдачи</label>
                <input class = "text" id="date_pas" />
              </div>

              <div class="form-column">
                <label class = "text" for="scan_pas">Скан</label>
                <input type="file" class = "text" id="scan_pas" style={{display: "none"}} />
                <div class="form-row" id ="scan_pas_exist" style={{display: "none"}}>
                  <button type="submit" class="submit text" onClick={this.downloadPassportScan} >Скачать</button>
                  <button type="submit" class="submit text" style={{backgroundColor:"red"}} onClick={this.deletePassportScan}>Удалить</button>
                </div>
              </div>
              


              <div class="form-row">
                <button type="submit" class="submit text" onClick={this.createOrEditPassport}>OK</button>
                <button type="submit" class="submit text" style={{backgroundColor:"red"}} onClick={this.deletePassport}>Удалить</button>
              </div>

            </div>
          

            <button id="createPassport" type="submit" class="submit text" style={{backgroundColor:"blue", display:"none"}} onClick={this.createPassportButton}>Создать</button>
          </form>
        </div>
      </div>
    );
  }
}

export default Documents;