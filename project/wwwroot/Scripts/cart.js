function deleteRow(product) {
    const rows = document.querySelectorAll('.item-row');
    rows.forEach(row => {
        if (row.querySelector('.itemName').textContent === product.productName) {
            row.remove();
            return;
        }
    });
}

const removeProduct = (product) => {
    let cart = sessionStorage.getItem('cart') ? JSON.parse(sessionStorage.getItem("cart")) : [];
    let newCart = cart.filter(p => product.productName != p.productName)
    sessionStorage.setItem("cart", JSON.stringify(newCart));
    deleteRow(product)
}
function createProductRows(products) {
    const template = document.getElementById('temp-row');

    const uniqueProducts = [];
    const productCounts = {};

    products.forEach(product => {
        const productKey = JSON.stringify(product);
        if (!uniqueProducts.includes(productKey)) {
            uniqueProducts.push(productKey);
            productCounts[productKey] = 1;
        } else {
            productCounts[productKey]++;
            return;
        }
    });

    uniqueProducts.forEach(p => {
        const clone = document.importNode(template.content, true);
        const product = JSON.parse(p)
        clone.querySelector('.imageColumn a').href = 'Images/' +product.imageUrl;
        clone.querySelector('.imageColumn img').src = 'Images/' +product.imageUrl;
        clone.querySelector('.itemName').textContent = product.productName;
        clone.querySelector('.price').textContent = product.price*productCounts[p]+'$';
        clone.querySelector('.quantity').textContent = productCounts[p];
        clone.querySelector('#remove').addEventListener('click', () => removeProduct(product))

        document.getElementById('tbody').appendChild(clone);
    });
}

const clear = () => {
    const elementsToRemove = document.querySelectorAll('.item-row');
    elementsToRemove.forEach(element => element.remove());
    sessionStorage.removeItem("cart")
}

const placeOrder = async () => {
    const user = sessionStorage.getItem("user")
    if(!user){
        alert("you need to sign in")
            return
    }
    const uniqueProducts = [];
    const productCounts = {};

    prodCart.forEach(product => {
        
        if (!uniqueProducts.includes(product.productId)) {
            uniqueProducts.push(product.productId);
            productCounts[product.productId] = 1;
        } else {
            productCounts[product.productId]++;
            return;
        }
    });
    let orderItems = []
    let sum = 0;
    uniqueProducts.forEach(up => {
        orderItems.push({ productId: up, quantity: productCounts[up] })
    })
    const currentDate = new Date();

    const order = {
        orderDate: currentDate,
        orderSum: 0,
        orderItems,
        userId: JSON.parse(user).id
    }
    debugger
    const res = await fetch("api/orders", {
        method: "POST",
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(order)
    });
    if (res.ok) {
        alert('created successfully!')
        clear()
    }
    else alert('error')
}

const prodCart = sessionStorage.getItem('cart') ? JSON.parse(sessionStorage.getItem("cart")) : [];
createProductRows(prodCart)