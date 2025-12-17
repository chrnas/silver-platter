import LandingPage from './Pages/LandingPage';
import MyPage from './Pages/MyPage';
import Browse from './Pages/Browse';
import RestaurantSpecific from './Pages/RestaurantSpecific';
import Restaurants from './Pages/Restaurants';
import Header from './Pages/Header';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { useMemo, useState } from 'react';
import { restaurantService } from './service/RestaurantService';
import type { Restaurant } from './Types/Restaurant';
import './App.css';

function App() {
    const [restaurants, setRestaurants] = useState<Restaurant[]>([])
    useMemo(() => {
        restaurantService.getAll().then(data => {
            setRestaurants(data);
        })
    }, [])

    return (
        <div className="Scaffold">
            <Router>
                <Header/>
                <Routes>
                    <Route path="/" element={<LandingPage />} />
                    <Route path="/MyPage" element={<MyPage />} />
                    <Route path="/Browse" element={<Browse />} />
                    <Route path='/Restaurants' element={<Restaurants/>}>
                    {restaurants.map(restaurant => { // Create one route per restaurant in database
                        return (
                            <Route path={restaurant.name} element={<RestaurantSpecific restaurant={restaurant}/>}/>
                        )
                    })}
                    </Route>    
                </Routes>
            </Router>
        </div>
    )
}

export default App;