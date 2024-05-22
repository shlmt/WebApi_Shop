
document.getElementById('ItemsCountText').textContent =sessionStorage.getItem('cart')? JSON.parse(sessionStorage.getItem("cart")).length : 0;


getAllProduct = async () => {
    const response = await fetch('api/products')
    const products = await response.json();
    drawProducts(products)
}

addToCart = (product) => {
    let cart = sessionStorage.getItem('cart')?JSON.parse(sessionStorage.getItem("cart")) : [];
    cart.push(product);
    sessionStorage.setItem("cart", JSON.stringify(cart));
    document.getElementById('ItemsCountText').textContent = cart.length;
}

removeAllCart = () => {
    sessionStorage.removeItem("cart");
    document.getElementById('ItemsCountText').textContent = 0;
}

const drawProducts=(products) =>{
    const template = document.getElementById('temp-card');

    products.forEach(product => {
        const clone = document.importNode(template.content, true);

        const img = clone.querySelector('img');
        img.src = 'Images/'+product.imageUrl;

        clone.querySelector('h1').textContent = product.productName
        clone.querySelector('.price').textContent = 'Price: ' + product.price;
        clone.querySelector('.description').textContent = product.categoryName;

        // You can add event listener to the button here if needed
        clone.querySelector('button').addEventListener('click', () => addToCart(product));

        document.body.appendChild(clone);
    });
}

const  getAllCategories = async() => {
    const response = await fetch('api/categories')
    const categories = await response.json();
    drawCategories(categories)
}

const drawCategories = (categories) => {
    const template = document.getElementById('temp-category');

    categories.forEach(category => {
        const clone = document.importNode(template.content, true);
        const checkbox = clone.querySelector('.opt');

        checkbox.id = category.categoryId;
        checkbox.value = category.categoryId;
        clone.querySelector('label').htmlFor = category.id;
        clone.querySelector('.OptionName').textContent = category.categoryName;
        clone.querySelector('.Count').textContent = category.count;

        document.getElementById('categoryList').appendChild(clone);
    });
}

const clear = () => {
    const elementsToRemove = document.querySelectorAll('.card');
    elementsToRemove.forEach(element => element.remove());
}

const  filterProducts =async () => {
    let stringUrl = 'api/products?'
    let min = document.getElementById('minPrice').value
    let max = document.getElementById('maxPrice').value
    let desc = document.getElementById('nameSearch').value
    if (min) stringUrl += 'minPrice=' + min+"&"
    if (max) stringUrl += 'maxPrice=' + max+"&"
    if (desc) stringUrl += 'description=' + desc + "&"
    let checks = document.getElementsByClassName('opt')
    let arr = [...checks]
    arr.forEach(c => {
        if (c.checked)
            stringUrl += 'category=' + c.id + "&"
    })
    const response = await fetch(stringUrl)
    const products = await response.json();
    clear()
    drawProducts(products)
}

getAllProduct()
getAllCategories()