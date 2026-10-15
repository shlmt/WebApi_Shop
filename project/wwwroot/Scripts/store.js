const currentUser = (() => {
    try {
        return JSON.parse(sessionStorage.getItem("user"));
    } catch {
        return null;
    }
})();

const userName = document.getElementById("userName");
if (currentUser && userName) {
    const firstName = currentUser.firstName || currentUser.FirstName;
    const lastName = currentUser.lastName || currentUser.LastName;
    const email = currentUser.email || currentUser.Email;
    userName.textContent = `שלום, ${[firstName, lastName].filter(Boolean).join(" ") || email || "אורח"}`;
    userName.hidden = false;
}

const accountLink = document.getElementById("accountLink");
if (currentUser && accountLink) {
    accountLink.href = "update.html";
}

if (currentUser) {
    const profile = {
        email: currentUser.email || currentUser.Email,
        firstName: currentUser.firstName || currentUser.FirstName,
        lastName: currentUser.lastName || currentUser.LastName
    };

    Object.entries(profile).forEach(([field, value]) => {
        const input = document.getElementById(field);
        if (input && value) input.value = value;
    });
}