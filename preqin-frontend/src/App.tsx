import React from 'react';
import { Route, BrowserRouter as Router, Routes } from 'react-router-dom';
import Commitments from './components/Commitments';
import InvestorList from './components/InvestorList';

const App: React.FC = () => {
    return (
        <Router>
            <Routes>
                {/* Route for showing the investor list */}
                <Route path="/" element={<InvestorList />} />

                {/* Route for showing commitments based on investor id */}
                <Route path="/commitments/:investorId" element={<Commitments />} />
            </Routes>
        </Router>
    );
};

export default App;
