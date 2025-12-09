import LandingPage from './Pages/LandingPage';
import MyPage from './Pages/MyPage';
import Browse from './Pages/Browse';
import RestaurantSpecific from './Pages/RestaurantSpecific';
import Restaurants from './Pages/Restaurants';
import Header from './Pages/Header';
import TestingPage from './Pages/TestingPage';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import './App.css';

function App() {
    return (
        <div className="Scaffold">
            <Router>
                <Header/>
                <Routes>
                    <Route path="/" element={<LandingPage />} />
                    <Route path="/MyPage" element={<MyPage />} />
                    <Route path="/Browse" element={<Browse />} />
                    <Route path='/Restaurants' element={<Restaurants/>}>
                    <Route path='Template' element={<RestaurantSpecific/>}/>
                    </Route>    
                    <Route path='/TestingPage' element={<TestingPage/>}/>
                    
                </Routes>
            </Router>
        </div>
    )
}

export default App;