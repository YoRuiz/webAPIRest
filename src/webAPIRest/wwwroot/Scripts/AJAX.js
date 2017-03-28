window.addEventListener("load", start);
window.addEventListener("load", listar);


function start() {   
    document.getElementById("btnInsertar").addEventListener("click", insertar);
    document.getElementById("btnActualizar").addEventListener("click", actualizar);
    document.getElementById("btnCancelarActualizar").addEventListener("click", cancelarActualizar);
    document.getElementById("btnCancelarInsercion").addEventListener("click", cancelar);
}
//declaro una variable que luego será la persona seleccionada
var p;


function listar() {
    //1 - instanciamos el objeto
    var xmlObject = new XMLHttpRequest();

    //2 - definimos el método open, get y la source de los datos
    xmlObject.open("GET", "../api/Values", true);

    //3 - definir cabeceras, en este caso nada por ser GET

    //4 - si es asincrono definir que hacemos cuando cambia el estado
    xmlObject.onreadystatechange = function () {
        //alert(xmlObject.readyState);
        if (xmlObject.readyState < 4) {
            document.getElementById("contenedor").innerHTML = "Loading...";
        } else {
            if (xmlObject.readyState == 4 && xmlObject.status == 200) {
                var respuestaJSON = JSON.parse(xmlObject.responseText);
                escribirPersonas(respuestaJSON);
            }
        }
    };
    //5 - envio de la solicitud
    xmlObject.send();
}
function getById(id) {
    //1 - instanciamos el objeto
    var xmlObject = new XMLHttpRequest();

    //2 - definimos el método open, get y la source de los datos
    xmlObject.open("GET", "../api/Values/"+id, true);

    //3 - definir cabeceras, en este caso nada por ser GET

    //4 - si es asincrono definir que hacemos cuando cambia el estado
    xmlObject.onreadystatechange = function () {
        //alert(xmlObject.readyState);
        if (xmlObject.readyState < 4) {
            document.getElementById("contenedor").innerHTML = "Loading...";
        } else {
            if (xmlObject.readyState == 4 && xmlObject.status == 200) {
                var respuestaJSON = JSON.parse(xmlObject.responseText);
                p = new Persona();
                p.id = respuestaJSON.id;
                document.getElementById("inputNombreActualizar").value =  respuestaJSON.nombre;
                document.getElementById("inputApellidosActualizar").value = respuestaJSON.apellidos;
                document.getElementById("inputFechaActualizar").value = respuestaJSON.fechaNac.substr(0, respuestaJSON.fechaNac.indexOf('T'));
                document.getElementById("inputTelefonoActualizar").value = respuestaJSON.telefono;
                document.getElementById("inputDireccionActualizar").value = respuestaJSON.direccion;
            }
        }
    };
    //5 - envio de la solicitud
    
    xmlObject.send();
}
function borrar(id) {
    //1 - instanciamos el objeto
    var xmlObject = new XMLHttpRequest();
    //2 - definimos el método open, get y la source de los datos
    xmlObject.open("DELETE", "../api/Values/"+id, true);

    //3 - definir cabeceras, en este caso nada por ser GET

    //4 - si es asincrono definir que hacemos cuando cambia el estado
    xmlObject.onreadystatechange = function () {
        if (xmlObject.readyState < 4) {
            document.getElementById("contenedor").innerHTML = "Loading...";
        } else {
            //alert(xmlObject.readyState);
            if (xmlObject.readyState == 4) {
                //6 - Trabajamos con los datos recibidos
                switch (xmlObject.status) {
                    case 200:
                        listar();
                        pintaBienMal(1, "Borrado correcto");
                        break;
                    case 204:
                        listar();
                        pintaBienMal(0, "No content");
                        break;
                    case 404:
                        listar();
                        pintaBienMal(0, "Objeto no encontrado");
                        break;
                    default:
                        listar();
                        pintaBienMal(0, "Error del servidor");
                        break;
                }
            }
        }
    };

    //5 - envio de la solicitud
    xmlObject.send();
}
function insertar() {
    //1. Instanciar objeto XMLHttpRequest
    var xml = new XMLHttpRequest();

    //2. Definir método open
    xml.open("POST", "../api/Values");

    p = new Persona();
    p.nombre = document.getElementById("inputNombre").value;
    p.apellidos = document.getElementById("inputApellidos").value;
    p.fechaNac = document.getElementById("inputFecha").value+"T00:00:00";
    p.telefono = document.getElementById("inputTelefono").value;
    p.direccion = document.getElementById("inputDireccion").value;
    //3. Definir cabeceras
    xml.setRequestHeader("Content-Type", "application/json");

    //4. Definir qué hacer cuando va cambiando el estado
    //5. Enviar la solicitud, send tiene parámetros opcionales

    if (p.fechaNac == "" || p.nombre == "" || p.apellidos == null) {
        pintaBienMal(0, "Error del servidor");
    } else {
        
        xml.send(JSON.stringify(p));
        setTimeout(listar, 1000);
        pintaBienMal(1, "Inserción correcta");
    }
    borrarInsert();

    $("#dialogInsertar").parent().hide();
   
    //listar();
}
function actualizar() {
    //1. Instanciar objeto XMLHttpRequest
    var xml = new XMLHttpRequest();
    p.nombre = document.getElementById("inputNombreActualizar").value;
    p.apellidos = document.getElementById("inputApellidosActualizar").value;
    p.fechaNac = document.getElementById("inputFechaActualizar").value;
    p.telefono = document.getElementById("inputTelefonoActualizar").value;
    p.direccion = document.getElementById("inputDireccionActualizar").value;

    //2. Definir método open
    xml.open("PUT", "../api/Values/"+p.id,p);

    //3. Definir cabeceras
    xml.setRequestHeader("Content-Type", "application/json");

    //4. Definir qué hacer cuando va cambiando el estado
    //5. Enviar la solicitud, send tiene parámetros opcionales

    if (p.fechaNac == "" || p.nombre == "" || p.apellidos == null) {
        pintaBienMal(0, "Error del servidor");
       //listar();
    } else {
        pintaBienMal(1, "Actualización correcta");
        xml.send(JSON.stringify(p));
       // listar();
        
    }
    $("#dialogActualizar").parent().hide();

    setTimeout(listar, 1000);
    xml.send(JSON.stringify(p));
}
function cancelarActualizar() {
    //$("#dialogActualizar").parent().hide();
    $("#dialogActualizar").parent().hide();
    
}
function cancelar() {
    borrarInsert();

    $("#dialogInsertar").parent().hide();
}
function borrarInsert() {
    document.getElementById("inputNombre").value = "";
    document.getElementById("inputApellidos").value = "";
    document.getElementById("inputFecha").value = "";
    document.getElementById("inputTelefono").value = "";
    document.getElementById("inputDireccion").value = "";
}
function escribirPersonas(respuestaXML) {
    var tabla = document.createElement("TABLE");
    var primeraFila = document.createElement("tr");
    var td1primeraFila = document.createElement("td");
    var texto1 = document.createTextNode("Id");
    td1primeraFila.appendChild(texto1);
    var td2primeraFila = document.createElement("td");
    var texto2 = document.createTextNode("Nombre");
    td2primeraFila.appendChild(texto2);
    var td3primeraFila = document.createElement("td");
    var texto3 = document.createTextNode("Apellidos");
    td3primeraFila.appendChild(texto3);

    primeraFila.appendChild(td1primeraFila);
    primeraFila.appendChild(td2primeraFila);
    primeraFila.appendChild(td3primeraFila);
    primeraFila.setAttribute("class", "cabecera");
    tabla.appendChild(primeraFila);
    for (i = 0; i < respuestaXML.length; i++) {
        var fila = document.createElement("tr");

        var celdaId = document.createElement("td");
        var textoCeldaId = document.createTextNode(respuestaXML[i].id);
        celdaId.appendChild(textoCeldaId);
        var celdaNombre = document.createElement("td");
        var textoCeldaNombre = document.createTextNode(respuestaXML[i].nombre);
        celdaNombre.appendChild(textoCeldaNombre);
        var celdaApellidos = document.createElement("td");
        var textoCeldaApellidos = document.createTextNode(respuestaXML[i].apellidos);
        celdaApellidos.appendChild(textoCeldaApellidos);
        //celda de botones - - por ahora borrar
        var celdaBotones = document.createElement("td");
        var botonBorrar = document.createElement("button");

        //asigna el id del boton de borrar con el nombre + el id de la persona
        botonBorrar.id = "btnBorrar" + respuestaXML[i].id;
        botonBorrar.title = "Pulse para borrar";
        var imagenBorrar = document.createElement("img");
        imagenBorrar.src = "../images/garbage.png";
        botonBorrar.appendChild(imagenBorrar);
        celdaBotones.appendChild(botonBorrar);
        
        var botonActualizar = document.createElement("button");
        botonActualizar.title = "Pulse para modificar";
        //asigna el id del boton de borrar con el nombre + el id de la persona
        botonActualizar.id = "btnActualizar" + respuestaXML[i].id;
       
        var imagenActualizar = document.createElement("img");
        imagenActualizar.src = "../images/quill-drawing-a-line.png";
        botonActualizar.appendChild(imagenActualizar);
        celdaBotones.appendChild(botonActualizar);
        celdaBotones.width ="auto";

        fila.appendChild(celdaId);
        fila.appendChild(celdaNombre);
        fila.appendChild(celdaApellidos);
        fila.appendChild(celdaBotones);


        tabla.appendChild(fila);
    }
    //muestra la tabla, si no está esto no muestra nada, solo el mensaje de loading
    document.getElementById("contenedor").innerHTML = "";
    document.getElementById("contenedor").appendChild(tabla);
    for (i = 0; i < respuestaXML.length; i++) {
        //añade el event listener de los botones
        document.getElementById("btnBorrar" + respuestaXML[i].id).addEventListener("click", dialog);
        document.getElementById("btnActualizar" + respuestaXML[i].id).addEventListener("click", abrir);
    }   
}

class Persona {
    constructor(nombre, apellidos, id, fechaNac, direccion, telefono) {
        this.nombre = nombre;
        this.apellidos = apellidos;
        this.id = id;
        this.fechaNac = fechaNac;
        this.direccion = direccion;
        this.telefono = telefono;
    }
}
//este método pinta en los contenedores de error no success (no error), recibe 0 o 1 y una cadena
//0 - mensaje de error
//1 - mensaje de success
//cadena - texto a mostrar
// - - - - -- - - - - -- aprender a usar timeout
function pintaBienMal($i, $respuesta) {
    switch ($i) {
        case 0:
            document.getElementById("contenedorError").innerHTML = $respuesta;
            document.getElementById("contenedorError").style.opacity = "1";
            document.getElementById("contenedorBien").style.opacity = "0";
            setTimeout(function(){
                document.getElementById("contenedorError").style.opacity = "0";}, 4000);
            break;
        case 1:
            document.getElementById("contenedorBien").innerHTML = $respuesta;
            document.getElementById("contenedorBien").style.opacity = "1";
            document.getElementById("contenedorError").style.opacity = "0";
            setTimeout(function () {
                document.getElementById("contenedorBien").style.opacity = "0";
            }, 4000);
            break;
    }
}
function abrir() {
    $("#dialogActualizar").parent().show();
    $("#dialogActualizar").show();
        $("#dialogActualizar").dialog({
            width: "40%"
        });
        var id = this.id;
        id = id.replace('btnActualizar', '');
        getById(id);
        setTimeout(listar, 1000);
}

function dialog() {
    var botonBorrarSeguro = this.id;
    botonBorrarSeguro = botonBorrarSeguro.replace('btnBorrar', '');
    var dialog = $('#borrarSeguro').dialog();
    dialog.parent().show();
    dialog.show();
    $('#btnSi').unbind().click(function () {
        dialog.parent().hide();
        borrar(botonBorrarSeguro);
    });
    $('#btnNo').click(function () {
        dialog.parent().hide();
    });
}