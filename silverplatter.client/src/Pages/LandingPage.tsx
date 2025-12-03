import PopupRestaurantComp from '../Components/PopupRestaurantComp';
import './css/LandingPage.css'

function LandingPage() {


    return (
        <div className="LandingPage">
            <div className="Content">
                <PopupRestaurantComp/>
            </div>

            <div className="Background">
                <video autoPlay muted loop> 
                    <source src="src/assets/LandingBackground.mp4" type="video/mp4"/>
                </video>
            </div>
        </div>
    )
}

export default LandingPage;