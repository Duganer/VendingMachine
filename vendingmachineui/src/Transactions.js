import React, { useState, useEffect } from 'react';
import PurchaseForm from './PurchaseForm';
import './App.css';

const Transactions = () => {
    const [transactions, setTransactions] = useState(null);

    useEffect(() => {
        fetchTransactions();
    }, []);

    const fetchTransactions = async () => {
        const response = await fetch('/VendingMachine/transactions');
        const data = await response.json();
        setTransactions(data);
    };

    const handlePurchase = async (purchaseData) => {
        const response = await fetch('/VendingMachine/purchase', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(purchaseData),
        });

        if (response.ok) {
            fetchTransactions();
        } else {
            console.error('Purchase failed:', response.statusText);
        }
    };

    if (!transactions) {
        return <p>Loading...</p>;
    }

    return (
        <div className="Transactions">
            <h3>Transactions</h3>
            <PurchaseForm onPurchase={handlePurchase} />
            <table className="table">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Items Purchased</th>
                        <th>Payment Amount</th>
                        <th>Payment Type</th>
                        <th>Timestamp</th>
                        <th>Refund</th>
                    </tr>
                </thead>
                <tbody>
                    {transactions.map((transaction) => (
                        <tr key={transaction.id}>
                            <td>{transaction.id}</td>
                            <td>
                                {transaction.itemsPurchased.map((itemPurchase) => (
                                    <div key={itemPurchase.item.name}>
                                        {itemPurchase.quantity} x {itemPurchase.item.name}
                                    </div>
                                ))}
                            </td>
                            <td>{transaction.payment.amount}</td>
                            <td>{transaction.payment.paymentType}</td>
                            <td>{transaction.timestamp}</td>
                            <td>{transaction.refundAmount}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default Transactions;
