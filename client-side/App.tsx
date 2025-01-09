import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import TheatreList from './src/Paginas/TheatresList';

const App: React.FC = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<TheatreList />} />
            </Routes>
        </Router>
    );
};

export default App;