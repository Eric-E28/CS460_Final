//async function fetchOrdersByStation(stationName) {
//    try {
//        const response = await fetch(`https://localhost:7293/api/order/station/${stationName}`);
//        const data = await response.json();

//        return data;
//    } catch (error) {
//        console.error('Error fetching orders by station:', error);
//        return [];
//    }
//}

//async function displayOrdersByStation(stationName, tabId) {
//    const orders = await fetchOrdersByStation(stationName);

//    const tabContent = document.getElementById(tabId);
//    tabContent.innerHTML = '';

//    orders.forEach(order => {
//        const orderCard = document.createElement('div');
//        orderCard.classList.add('card', 'mb-3'); // Adding Bootstrap classes to create a card layout

//        // Create card body to display order details
//        const cardBody = document.createElement('div');
//        cardBody.classList.add('card-body');

//        // Example order details to display
//        cardBody.innerHTML = `
//            <p class="card-text">Item: ${order.name}</p>
//            <p class="card-text">Quantity: ${order.qty}</p>
//            <p class="card-text">Completed: ${order.completed ? 'Yes' : 'No'}</p>
//        `;

//        orderCard.appendChild(cardBody);
//        tabContent.appendChild(orderCard);
//    });
//}

//// Event listeners for tab change
//document.getElementById('home2-tab').addEventListener('click', () => displayOrdersByStation('Drinks', 'home2'));
//document.getElementById('profile2-tab').addEventListener('click', () => displayOrdersByStation('Kitchen', 'profile2'));
//document.getElementById('contact2-tab').addEventListener('click', () => displayOrdersByStation('Dessert', 'contact2'));

//// Initial display
//displayOrdersByStation('Drinks', 'home2');

async function fetchOrdersByStation(stationName) {
    try {
        const response = await fetch(`https://localhost:7293/api/order/station/${stationName}`);
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching orders by station:', error);
        return [];
    }
}

async function updateOrderCompletionStatus(orderId) {
    try {
        await fetch(`https://localhost:7293/api/order/completed/${orderId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ completed: true })
        });
        console.log(`Order ${orderId} marked as completed.`);
    } catch (error) {
        console.error(`Error updating order ${orderId} status:`, error);
    }
}

async function displayOrdersByStation(stationName, tabId) {
    const orders = await fetchOrdersByStation(stationName);
    const tabContent = document.getElementById(tabId);

    if (!tabContent.querySelector('table')) {
        const table = document.createElement('table');
        table.classList.add('table', 'table-borderless', 'table-hover');

        const tableHeader = document.createElement('thead');
        tableHeader.innerHTML = `
            <tr>
            </tr>
        `;
        table.appendChild(tableHeader);

        const tableBody = document.createElement('tbody');
        table.appendChild(tableBody);

        tabContent.appendChild(table);
    }

    const tableBody = tabContent.querySelector('tbody');
    tableBody.innerHTML = ''; // Clear existing table body

    orders.forEach(order => {
        const tableRow = document.createElement('tr');

        const checkboxCell = document.createElement('td');
        const checkbox = document.createElement('input');
        checkbox.type = 'checkbox';

        const itemNameCell = document.createElement('td');
        itemNameCell.textContent = order.name;

        const quantityCell = document.createElement('td');
        quantityCell.textContent = order.qty;

        checkboxCell.appendChild(checkbox);
        tableRow.appendChild(checkboxCell);
        tableRow.appendChild(itemNameCell);
        tableRow.appendChild(quantityCell);

        tableBody.appendChild(tableRow);

        checkbox.addEventListener('change', function () {
            if (checkbox.checked) {
                const confirmationModal = new bootstrap.Modal(document.getElementById('confirmationModal'), {
                    keyboard: false
                });
                confirmationModal.show();

                const confirmButton = document.getElementById('confirmButton');
                confirmButton.addEventListener('click', function () {
                    updateOrderCompletionStatus(order.id);
                    tableBody.removeChild(tableRow);
                    confirmationModal.hide();
                });

                const cancelButton = document.querySelector('#confirmationModal .btn-secondary');
                cancelButton.addEventListener('click', function () {
                    checkbox.checked = false;
                    confirmationModal.hide();
                });
            }
        });
    });
}

document.getElementById('home2-tab').addEventListener('click', () => displayOrdersByStation('Drinks', 'home2'));
document.getElementById('profile2-tab').addEventListener('click', () => displayOrdersByStation('Kitchen', 'profile2'));
document.getElementById('contact2-tab').addEventListener('click', () => displayOrdersByStation('Dessert', 'contact2'));

displayOrdersByStation('Drinks', 'home2');




