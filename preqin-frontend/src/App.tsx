import React, { useState } from 'react';
import Commitments from './components/Commitments';
import InvestorList from './components/InvestorList';

const App: React.FC = () => {
    const [selectedInvestorId, setSelectedInvestorId] = useState<number | null>(null);

    return (
        <div>
            <InvestorList onInvestorSelect={setSelectedInvestorId} />
            {selectedInvestorId && <Commitments investorId={selectedInvestorId} />}
        </div>
    );
};

export default App;
