
function PageLogo() {
    return (
        <div style={{
            position: "absolute",
            display: "flex",
            flexDirection: "row",
            height: "4rem",
            width: "20rem",
            marginLeft: "2%",
            textAlign: "center",
            alignItems: "center"
        }}>
            <img src="src/assets/silverplatter_logo.png" alt="" style={{
                width: "20%"
            }}/>
            <h1 style={{marginLeft: "5%"}}>SilverPlatter</h1>
        </div>
    )
}

export default PageLogo;