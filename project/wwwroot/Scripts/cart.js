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
        clone.querySelector('.itemName').textContent = product.productName;
        clone.querySelector('.price').textContent = product.price*productCounts[p]+'$';
        clone.querySelector('.quantity').textContent = productCounts[p];
        clone.querySelector('#remove').addEventListener('click', () => removeProduct(product))

        document.getElementById('tbody').appendChild(clone);
    });
}

const prodCart = sessionStorage.getItem('cart') ? JSON.parse(sessionStorage.getItem("cart")) : [];
createProductRows(prodCart)