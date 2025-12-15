
import { useNavigate } from 'react-router-dom';
import type { Restaurant } from '../Types/Restaurant';
import './css/popupRestaurantComp.css'

function SkipRestaurant(restaurant : Restaurant) {
    // Heavily discourage the algorithm to show the same restaurant in the next 10 suggestions
    // Show next
}

function PopupRestaurantComp(props : { restaurant : Restaurant}) {
    let nav = useNavigate();
    let ref = "Template"

    return (
        <div className='Frame'>
            <button className='Button' onClick={() => SkipRestaurant(props.restaurant)}>Skip</button>
            <div className="PopupRestaurant">
                <div className='Info'>
                    <div>
                        <h1 className='PR_Name'>{props.restaurant.name}</h1>
                        <p className='PR_Description'>{props.restaurant.description}</p>
                    </div>
                </div>
                <img
                    src="src/assets/pexels-life-of-pix-67468.jpg" 
                />
            </div>
            <button className='Button' onClick={() => nav(ref)}>Go to</button>
        </div>
    )
}

export default PopupRestaurantComp;