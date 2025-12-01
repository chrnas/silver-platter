import Header from './Pages/Header';
import LandingPage from './Pages/LandingPage';
import MyPage from './Pages/MyPage';
import Browse from './Pages/Browse'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';import './App.css';

function App() {
    return (
        <div id="Scaffold">
            <Router>
                <Header/>
                <Routes>
                    <Route path="/" element={<LandingPage />} />
                    <Route path="/MyPage" element={<MyPage />} />
                    <Route path="/Browse" element={<Browse />} />
                </Routes>
            </Router>
        </div>
    )
}

export default App;