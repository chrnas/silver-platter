import { useNavigate } from 'react-router-dom';
import type { Restaurant } from '../Types/Restaurant';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faStar } from '@fortawesome/free-solid-svg-icons';
import './css/RestaurantButton.css';

function RestaurantButton(props : { restaurant : Restaurant}) {
    let nav = useNavigate();
    return (
        <button onClick={() => nav("/Restaurants/" + props.restaurant.name)} id='SingleRestaurantButton'>
            <div className='RestaurantName'>
                <h2>{props.restaurant.name}</h2>
            </div>
            <div className="badge" style={{backgroundColor: "rgba(0, 0, 0, 0.5)"}}>
                {Array.from({length: props.restaurant.rating}).map((_, i) => (
                    <FontAwesomeIcon key={i} icon={faStar} />
                ))}
            </div>
            <div className="badge" style={{color: 'white', backgroundColor: "rgba(0, 0, 0, 0.5)"}}>
                {"<"}{props.restaurant.budget} SEK
            </div>
        </button>
    )
}

export default RestaurantButton;