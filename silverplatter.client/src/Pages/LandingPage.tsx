

function LandingPage() {


    return (
        <div style={{
            width: "100vw",
            display: "flex",
            flexDirection: "column",
            position: "relative",
        }}>
            <div id="Content" style={{
                backgroundColor:"rgba(0, 0, 0, 0.5)",
                height: "100vh",
                order: 1,
                zIndex: 2,
                display: "flex",
                alignItems: "center",
                justifyContent: "center"
            }}>
                <img 
                    src="src/assets/claim-your-i-was-here-button-v0-9tlo8368wdkf1.webp" 
                    style={{
                        maxWidth: "500px",
                        height: "auto"
                    }}
                />
            </div>

            <div id="Background" style={{
                order: 2, 
                zIndex: 1,
                marginTop: "-100vh"
            }}>
                <video autoPlay muted loop style={{
                    width: "100%",
                }}> 
                    <source src="src/assets/LandingBackground.mp4" type="video/mp4"/>
                </video>
            </div>
        </div>
    )
}

export default LandingPage;