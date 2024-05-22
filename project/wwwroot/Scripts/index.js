const login= async () => {
    const obj = {
        Email: document.getElementById("email").value,
        Password: document.getElementById("password").value
    }
    const res = await fetch("api/users/login", {
        method: "POST",
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(obj)
    });
    debugger
    const dataPost = await res.json();
    if (res.ok) {
        sessionStorage.setItem("user", JSON.stringify(dataPost));
        window.location.href = 'products.html'
    }
    else alert('not authorized')
}

const register = async () => {
    const user = {
        FirstName: document.getElementById("firstName").value,
        LastName: document.getElementById("lastName").value,
        Email: document.getElementById("email2").value,
        Password: document.getElementById("password2").value
    }
    const res = await fetch("api/users/register", {
        method: "POST",
        headers: {
            'content-type':'application/json'
        },
        body: JSON.stringify(user)
    })
    const dataPost = await res.json();
    if (res.ok) {
        alert('created!')
        sessionStorage.setItem("user", JSON.stringify(dataPost));
        window.location.href = 'update.html'
    }
}

const update = async () => {
    const id = JSON.parse(sessionStorage.getItem("user")).id;
    const obj = {
        Id: id,
        Email: document.getElementById("email").value,
        Password: document.getElementById("password2").value,
        FirstName: document.getElementById("firstName").value,
        LastName: document.getElementById("lastName").value
    }
    const res = await fetch("api/users/update/"+id, {
        method: "PUT",
        headers: {
            'content-type':'application/json'
        },
        body: JSON.stringify(obj)
    })
    if (res.ok) {
        alert('updated!')
    }
    const dataPost = await res.json();
    if (dataPost) 
        sessionStorage.setItem("user", JSON.stringify(dataPost));
}

const checkStrength = async () => {
    const obj = {
        Pass: document.getElementById("password2").value,
    }
    const res = await fetch("api/users/passStrength", {
        method: "POST",
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(obj)
    })
    const dataPost = await res.json();
    document.getElementById("strong").innerHTML = dataPost
}