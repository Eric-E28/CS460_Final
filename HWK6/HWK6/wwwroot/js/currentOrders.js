//async function fetchOrders() {
//    try {
//        const response = await fetch('https://localhost:7293/api/order/iorders');
//        const data = await response.json();

//        // Call a function to display the orders in HTML
//        displayOrders(data);
//    } catch (error) {
//        console.error('Error fetching orders:', error);
//    }
//}

//// Function to display orders in HTML// Function to display orders in HTML
//function displayOrders(orders) {
//    const ordersContainer = document.getElementById('holders');

//    orders.forEach(order => {
//        // Create Bootstrap row for each order
//        const row = document.createElement('div');
//        row.classList.add('row', 'mt-4', 'd-flex', 'flex-row');

//        const colDiv = document.createElement('div');
//        colDiv.classList.add('col-lg-5', 'mx-auto');

//        const orderCard = document.createElement('div');
//        orderCard.classList.add('card', 'rounded', 'border-0', 'shadow-sm', 'position-relative', 'mb-3');

//        // Creating card header
//        const cardHeader = document.createElement('div');
//        cardHeader.classList.add('card-header', 'bg-dark', 'text-white');
//        cardHeader.innerHTML = `<strong>${order.customerName}</strong>`;
//        orderCard.appendChild(cardHeader);

//        // Display common order details inside the card
//        const orderDetails = document.createElement('div');
//        orderDetails.classList.add('card-body', 'p-5');

//        // Display checklist in the card body
//        for (let i = 0; i < order.name.length; i++) {
//            const itemDetails = document.createElement('div');
//            itemDetails.classList.add('d-flex', 'justify-content-between', 'mb-3');

//            const checkboxInput = document.createElement('input');
//            checkboxInput.classList.add('form-check-input');
//            checkboxInput.type = 'checkbox';
//            checkboxInput.id = `flexCheck${i + 1}`;

//            const label = document.createElement('label');
//            label.classList.add('form-check-label');
//            label.setAttribute('for', `flexCheck${i + 1}`);
//            label.innerHTML = `
//                <span class="fst-italic">${order.name[i]}</span>
//                <span class="ms-3">Qty : ${order.qty[i]}</span>
//                <span class="ms-3">$${order.price[i]}</span>
//                <span class="ms-3">${order.completionStatus[i]}</span>
//            `;

//            itemDetails.appendChild(checkboxInput);
//            itemDetails.appendChild(label);

//            // Append each item details to the same card body
//            orderDetails.appendChild(itemDetails);
//        }

//        // Footer for the card
//        const cardFooter = document.createElement('div');
//        cardFooter.classList.add('card-footer', 'text-muted');
//        cardFooter.innerHTML = 'Total Price : $' + order.totalPrice;

//        // Append the header, body, and footer to the order card
//        orderCard.appendChild(cardHeader);
//        orderCard.appendChild(orderDetails);
//        orderCard.appendChild(cardFooter);

//        colDiv.appendChild(orderCard);
//        row.appendChild(colDiv);

//        // Append each row to the orders container
//        ordersContainer.appendChild(row);
//    });
//}

//// Call the fetchOrders function when the page loads
//window.onload = function () {
//    fetchOrders();
//};

async function fetchOrders() {
    try {
        const response = await fetch('https://localhost:7293/api/order/iorders');
        const data = await response.json();

        // Call a function to display the orders in HTML
        displayOrders(data);
    } catch (error) {
        console.error('Error fetching orders:', error);
    }
}

// Function to display orders in HTML
function displayOrders(orders) {
    const ordersContainer = document.getElementById('holders');

    orders.forEach(order => {
        const row = document.createElement('div');
        row.classList.add('row', 'mt-4', 'd-flex', 'flex-row');

        const colDiv = document.createElement('div');
        colDiv.classList.add('col-lg-5', 'mx-auto');

        const orderCard = document.createElement('div');
        orderCard.classList.add('card', 'rounded', 'border-0', 'shadow-sm', 'position-relative', 'mb-3', 'card-hover');

        const currentTime = new Date();
        const orderTime = new Date(order.time);
        const timeDiff = currentTime - orderTime;
        const timeDiffInSeconds = Math.floor(timeDiff / 1000);

        let timeHistory;

        if (timeDiffInSeconds < 60) {
            timeHistory = `${timeDiffInSeconds} second${timeDiffInSeconds !== 1 ? 's' : ''} ago`;
        } else if (timeDiffInSeconds < 3600) {
            const minutes = Math.floor(timeDiffInSeconds / 60);
            timeHistory = `${minutes} minute${minutes !== 1 ? 's' : ''} ago`;
        } else {
            const hours = Math.floor(timeDiffInSeconds / 3600);
            timeHistory = `${hours} hour${hours !== 1 ? 's' : ''} ago`;
        }

        const cardHeader = document.createElement('div');
        cardHeader.classList.add('card-header', 'bg-dark', 'text-white');
        cardHeader.innerHTML = `<strong>${order.customerName} - ${order.dlvyName}</strong> (${timeHistory})`;
        orderCard.appendChild(cardHeader);

        const formattedTotalPrice = parseFloat(order.totalPrice).toFixed(2);
        const orderDetails = document.createElement('div');
        orderDetails.classList.add('card-body', 'p-5');

        const table = document.createElement('table');
        table.classList.add('table', 'table-borderless');

        const tableBody = document.createElement('tbody');

        function getIcon(status) {
            return status === 'Completed'
                ? '<i class="bi bi-check2-circle text-success"></i>' // Green check icon for completed
                : '<i class="bi bi-clock-history text-danger"></i>'; // Red clock icon for in-progress
        }

        for (let i = 0; i < order.name.length; i++) {
            const tableRow = document.createElement('tr');
            tableRow.innerHTML = `
                <td>${order.name[i]}</td>
                <td> x${order.qty[i]}</td>
                <td>$${order.price[i]}</td>
                <td>${getIcon(order.completionStatus[i])}</td>
            `;
            tableBody.appendChild(tableRow);
        }

        table.appendChild(tableBody);
        orderDetails.appendChild(table);

        const cardFooter = document.createElement('div');
        cardFooter.classList.add('card-footer', 'text-bold');
        cardFooter.innerHTML = 'Total Price - $' + formattedTotalPrice;

        orderCard.appendChild(cardHeader);
        orderCard.appendChild(orderDetails);
        orderCard.appendChild(cardFooter);

        colDiv.appendChild(orderCard);
        row.appendChild(colDiv);

        ordersContainer.appendChild(row);
    });
}

fetchOrders();





