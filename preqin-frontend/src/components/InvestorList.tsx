import axios from 'axios';
import React, { useEffect, useState } from 'react';
import './InvestorList.css';

// Define the interface for the Investor data
interface Investor {
    id: number;
    name: string;
    type: string;
    dateAdded: string;
    totalCommitments: number;
}

interface InvestorListProps {
    onInvestorSelect: (investorId: number) => void;
}

const InvestorList: React.FC<InvestorListProps> = ({ onInvestorSelect }) => {
    const [investors, setInvestors] = useState<Investor[]>([]);

    // Fetch investors data from the API when the component loads
    useEffect(() => {
        axios.get('/api/investor')
            .then(response => {
                setInvestors(response.data);
            })
            .catch(error => {
                console.error("There was an error fetching the investors!", error);
            });
    }, []);

    return (
        <div className="investor-list-container">
            <h1>Investors</h1>
            <table className="investor-table">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Type</th>
                        <th>Date Added</th>
                        <th>Total Commitment</th>
                    </tr>
                </thead>
                <tbody>
                    {investors.map(investor => (
                        <tr key={investor.id} onClick={() => onInvestorSelect(investor.id)}>
                            <td>{investor.id}</td>
                            <td>{investor.name}</td>
                            <td>{investor.type}</td>
                            <td>{new Date(investor.dateAdded).toLocaleDateString('en-US', { year: 'numeric', month: 'long', day: 'numeric' })}</td>
                            <td className="commitment-amount">{(investor.totalCommitments / 1000000000).toFixed(1)}B</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default InvestorList;
