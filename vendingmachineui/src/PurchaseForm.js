import React, { useState } from 'react';

const PurchaseForm = ({ onPurchase }) => {
    const [itemName, setItemName] = useState('');
    const [quantity, setQuantity] = useState(1);
    const [paymentAmount, setPaymentAmount] = useState(0);

    const handleSubmit = (e) => {
        e.preventDefault();
        const purchaseRequest = {
            items: [
                {
                    item: {
                        name: itemName,
                        price: 0
                    },
                    quantity: parseInt(quantity)
                }
            ],
            paymentType: 'Credit Card',
            paymentAmount: parseFloat(paymentAmount)
        };
        onPurchase(purchaseRequest);
        setItemName('');
        setQuantity(1);
        setPaymentAmount(0);
    };

    return (
        <form onSubmit={handleSubmit}>
            <label>
                Item:
                <input
                    type="text"
                    value={itemName}
                    onChange={(e) => setItemName(e.target.value)}
                    required
                />
            </label>
            <label>
                Quantity:
                <input
                    type="number"
                    value={quantity}
                    onChange={(e) => setQuantity(e.target.value)}
                    required
                    min="1"
                />
            </label>
            <label>
                Payment Amount:
                <input
                    type="number"
                    value={paymentAmount}
                    onChange={(e) => setPaymentAmount(e.target.value)}
                    required
                    step="0.01"
                />
            </label>
            <button type="submit">Purchase</button>
        </form>
    );
};

export default PurchaseForm;
