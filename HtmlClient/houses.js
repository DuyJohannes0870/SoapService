const URI = "https://bsite.net/duyhpk/HouseService.asmx";
const config = { headers : { 'Content-Type': 'text/xml' } };

function page_Load(){
    getAll();
}

function btnSearch_Click(){
    var keyword = document.getElementById("txtKeyword").value.trim();
    if(keyword.length > 0){
        search(keyword);
    }else{
        getAll();
    }
}

function lnkID_Click(id){
    getDetails(id);
}

function btnAdd_Click(){
    var newHouse = {
        ID:0,
        Owner: document.getElementById("txtOwner").value,
        Type: document.getElementById("txtType").value,
        Price: document.getElementById("txtPrice").value,
        Address: document.getElementById("txtAddress").value
    };
    addNew(newHouse);
}

function btnUpdate_Click(){
    var newHouse = {
        ID:document.getElementById("txtID").value,
        Owner: document.getElementById("txtOwner").value,
        Type: document.getElementById("txtType").value,
        Price: document.getElementById("txtPrice").value,
        Address: document.getElementById("txtAddress").value
    };
    update(newHouse);
}

function btnDelete_Click(){
    if(confirm("ARE YOU SURE?")){
        var id = document.getElementById("txtID").value;
        deletee(id);
    }
}

function getAll() {
    var body = `<?xml version="1.0" encoding="utf-8"?>
    <soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
      <soap:Body>
        <GetAll xmlns="http://duyhpk.org/" />
      </soap:Body>
    </soap:Envelope>`; 
    axios.post(URI + "?op=GetAll", body, config).then((response) => { 
        var xmlData = response.data;
        //alert(xmlData); // for DEBUG
        var jsonData = new X2JS ().xml_str2json(xmlData);
        //alert(JSON.stringify (jsonData)); // for DEBUG 
        var houses = jsonData.Envelope.Body.GetAllResponse.GetAllResult.House;
        //alert(JSON.stringify (nhanviens)); // for DEBUG 
        renderHouseList (houses); 
     });  
}

function search(keyword){
    var body = `<?xml version="1.0" encoding="utf-8"?>
    <soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
      <soap:Body>
        <Search xmlns="http://duyhpk.org/">
          <keyword>${keyword}</keyword>
        </Search>
      </soap:Body>
    </soap:Envelope>`;
    axios.post(URI + "?op=Search", body, config).then((response) => { 
        var xmlData = response.data;
        //alert(xmlData);
        var jsonData = new X2JS ().xml_str2json(xmlData);
        //alert(JSON.stringify (jsonData));
        var data = jsonData.Envelope.Body.SearchResponse.SearchResult.House;
        //alert(JSON.stringify (nhanviens));
        var houses=[];
        if(Array.isArray(data)) houses = data;
        else if(typeof(data) == "object") houses.push(data);
        renderHouseList(houses);
    });  
}

function getDetails(id){
    var body = `<?xml version="1.0" encoding="utf-8"?>
    <soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
      <soap:Body>
        <GetDetails xmlns="http://duyhpk.org/">
          <id>${id}</id>
        </GetDetails>
      </soap:Body>
    </soap:Envelope>`;
    axios.post(URI + "?op=GetDetails", body, config).then((response) => { 
        var xmlData = response.data;
        //alert(xmlData);
        var jsonData = new X2JS ().xml_str2json(xmlData);
        //alert(JSON.stringify (jsonData));
        var house = jsonData.Envelope.Body.GetDetailsResponse.GetDetailsResult;
        // var house = data;
        renderHouseDetails(house);
    });    
}

function addNew(newhouse){
    var body = `<?xml version="1.0" encoding="utf-8"?>
    <soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
      <soap:Body>
        <AddNew xmlns="http://duyhpk.org/">
          <newHouse>
            <ID>${newhouse.ID}</ID>
            <Owner>${newhouse.Owner}</Owner>
            <Type>${newhouse.Type}</Type>
            <Price>${newhouse.Price}</Price>
            <Address>${newhouse.Address}</Address>
          </newHouse>
        </AddNew>
      </soap:Body>
    </soap:Envelope>`;
    axios.post(URI + "?op=Add", body, config).then((response) => {
        var xmlData = response.data;
        var jsonData = new X2JS ().xml_str2json(xmlData);
        var data = jsonData.Envelope.Body.AddResponse.AddResult;
        var result = JSON.parse(data);
        if (result){
            getAll();
            clearTextboxes();
        }else{
            alert('sorry baby!!');
        }
    });
}

function update(newhouse){
    var body = `<?xml version="1.0" encoding="utf-8"?>
    <soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
      <soap:Body>
        <Update xmlns="http://duyhpk.org/">
          <newHouse>
            <ID>${newhouse.ID}</ID>
            <Owner>${newhouse.Owner}</Owner>
            <Type>${newhouse.Type}</Type>
            <Price>${newhouse.Price}</Price>
            <Address>${newhouse.Address}</Address>
          </newHouse>
        </Update>
      </soap:Body>
    </soap:Envelope>`;
    axios.post(URI + "?op=Update", body, config).then((response) => {
        var xmlData = response.data;
        var jsonData = new X2JS ().xml_str2json(xmlData);
        var data = jsonData.Envelope.Body.UpdateResponse.UpdateResult;
        var result = JSON.parse(data);
        if (result){
            getAll();
            clearTextboxes();
        }else{
            alert('sorry baby!!');
        }
    });
}

function deletee(id){
    var body = `<?xml version="1.0" encoding="utf-8"?>
    <soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
      <soap:Body>
        <Delete xmlns="http://duyhpk.org/">
          <ID>${id}</ID>
        </Delete>
      </soap:Body>
    </soap:Envelope>`;
    axios.post(URI + "?op=Delete", body, config).then((response) => {
        var xmlData = response.data;
        var jsonData = new X2JS ().xml_str2json(xmlData);
        var data = jsonData.Envelope.Body.DeleteResponse.DeleteResult;
        var result = JSON.parse(data);
        if (result){
            getAll();
            clearTextboxes();
        }else{
            alert('sorry baby!!');
        }
    });
}

function renderHouseList(houses){
    var rows = "";
    for(var House of houses){
        rows += "<tr onclick='lnkID_Click(" + House.ID + ")' style='cursor:pointer'>";
        rows += "<td>" + House.ID + "</td>";
        rows += "<td>" + House.Owner + "</td>";
        rows += "<td>" + House.Type + "</td>";
        rows += "<td>" + House.Price + "</td>";
        rows += "<td>" + House.Address + "</td>";
        rows += "</td>";
    }
    var header = "<tr><th>ID</th><th>Owner</th><th>Type</th><th>Price</th><th>Address</th></tr>";
    document.getElementById("lstHouses").innerHTML = header + rows;
}

function renderHouseDetails(house) {
    document.getElementById("txtID").value = house.ID;
    document.getElementById("txtOwner").value = house.Owner ;
    document.getElementById("txtType").value = house.Type;
    document.getElementById("txtPrice").value = house.Price;
    document.getElementById("txtAddress").value = house.Address;
}

function clearTextboxes(){
    document.getElementById("txtID").value ='';
    document.getElementById("txtOwner").value ='';
    document.getElementById("txtType").value ='';
    document.getElementById("txtPrice").value ='';
    document.getElementById("txtAddress").value ='';
}