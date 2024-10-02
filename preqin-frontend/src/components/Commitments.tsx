import axios from 'axios';
import React, { useEffect, useState } from 'react';
import './Commitments.css'; // Import the CSS for styling

// Define the interface for the Commitment data
interface Commitment {
    id: number;
    assetClass: string;
    amount: number;
    currency: string;
}

interface CommitmentsProps {
    investorId: number;
}

const Commitments: React.FC<CommitmentsProps> = ({ investorId }) => {
    const [commitments, setCommitments] = useState<Commitment[]>([]);
    const [filteredCommitments, setFilteredCommitments] = useState<Commitment[]>([]);
    const [totalByAssetClass, setTotalByAssetClass] = useState<Record<string, number>>({});
    const [totalCommitments, setTotalCommitments] = useState<number>(0);
    const [activeFilter, setActiveFilter] = useState<string | null>(null);

    // Fetch commitments data for the selected investor when the component loads
    useEffect(() => {
        axios.get(`/api/commitment/${investorId}`)
            .then(response => {
                const commitmentsData = response.data;

                // Calculate total commitments by asset class and overall
                const totals: Record<string, number> = {};
                let overallTotal = 0;
                commitmentsData.forEach((commitment: Commitment) => {
                    totals[commitment.assetClass] = (totals[commitment.assetClass] || 0) + commitment.amount;
                    overallTotal += commitment.amount;
                });

                setCommitments(commitmentsData);
                setFilteredCommitments(commitmentsData); // Initially show all commitments
                setTotalByAssetClass(totals);
                setTotalCommitments(overallTotal);
            })
            .catch(error => {
                console.error("There was an error fetching the commitments!", error);
            });
    }, [investorId]);

    const filterByAssetClass = (assetClass: string | null) => {
        let url = `/api/commitment/${investorId}`;
        if (assetClass) {
            url += `?assetClass=${encodeURIComponent(assetClass)}`;
        }
        axios.get(url)
            .then(response => {
                setFilteredCommitments(response.data); // Update the filtered commitments
                setActiveFilter(assetClass);
            })
            .catch(error => {
                console.error("There was an error fetching the commitments!", error);
            });
    };

    return (
        <div className="commitments-container">
            <h1>Commitments</h1>
            <div className="filter-buttons">
                <button className={activeFilter === null ? "active" : ""} onClick={() => filterByAssetClass(null)}>
                    All £{(totalCommitments / 1000000000).toFixed(1)}B
                </button>
                {Object.keys(totalByAssetClass).map(assetClass => (
                    <button
                        key={assetClass}
                        className={activeFilter === assetClass ? "active" : ""}
                        onClick={() => filterByAssetClass(assetClass)}
                    >
                        {assetClass} £{(totalByAssetClass[assetClass] / 1000000000).toFixed(1)}B
                    </button>
                ))}
            </div>
            <table className="commitments-table">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Asset Class</th>
                        <th>Currency</th>
                        <th>Amount</th>
                    </tr>
                </thead>
                <tbody>
                    {filteredCommitments.map(commitment => (
                        <tr key={commitment.id}>
                            <td>{commitment.id}</td>
                            <td>{commitment.assetClass}</td>
                            <td>{commitment.currency}</td>
                            <td>{(commitment.amount / 1000000).toFixed(1)}M</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default Commitments;
