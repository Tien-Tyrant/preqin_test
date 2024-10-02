import React from 'react';
import { Route, BrowserRouter as Router, Routes } from 'react-router-dom';
import Commitments from './components/Commitments';
import InvestorList from './components/InvestorList';

const App: React.FC = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<InvestorList />} />
                <Route path="/commitments/:investorId" element={<Commitments />} />
            </Routes>
        </Router>
    );
};

export default App;
