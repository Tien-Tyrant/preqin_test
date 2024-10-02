import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './InvestorList.css';

interface Investor {
    id: number;
    name: string;
    type: string;
    dateAdded: string;
    totalCommitments: number;
}

const InvestorList: React.FC = () => {
    const [investors, setInvestors] = useState<Investor[]>([]);
    const navigate = useNavigate(); 
    useEffect(() => {
        axios.get('/api/investor')
            .then(response => {
                setInvestors(response.data);
            })
            .catch(error => {
                console.error("There was an error fetching the investors!", error);
            });
    }, []);

    const handleInvestorClick = (investorId: number) => {
        navigate(`/commitments/${investorId}`);
    };

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
                        <tr key={investor.id} onClick={() => handleInvestorClick(investor.id)}>
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
