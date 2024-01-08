"use client"
import Logo from "@/components/logo";
import Home from "@/app/page";
import Link from "next/link";
import { useRouter } from "next/router";

const LoginPage = () => {
  const router = useRouter()

  const handleLogin = () => {
    router.push('/dashboard')
  }

  return (
    <div>
      <main className="flex">
        <div className="w-1/2">
          <div className="bg-background_foreground h-screen">
            <div className="p-4">
              <Logo />
            </div>
            <div className="flex items-center justify-center py-40">
              <h1 className="text-text_tertiary font-bold text-5xl font-work-sans mb-3 leading-tight">
                Elevate your
                <br /> Productivity with
                <br />{" "}
                <span className=" font-bold text-text_secondary">
                  Tracksify
                </span>
              </h1>
            </div>
          </div>
        </div>

        <div className=" w-1/2 flex flex-col px-20 py-40">
          <div>
            <h1 className="font-bold text-2xl">Get Started</h1>
            <form className="font-product-sans font-sm ">
              <div className=" mb-4 ">
                <label
                  className="block  text-sm font-bold mb-2"
                  htmlFor="email address"
                ></label>
                <input
                  className="  border rounded py-4 px-5 w-full  leading-tight outline-none "
                  id="email"
                  type="text"
                  placeholder="Email Address"
                />
              </div>
              <div className=" mb-4 ">
                <label
                  className="block  text-sm font-bold mb-2"
                  htmlFor="password"
                ></label>
                <input
                  className="  border rounded py-4 px-5 w-full  leading-tight outline-none "
                  id="password"
                  type="password"
                  placeholder="Password"
                />
              </div>

              <div className="">
                  <button onClick={handleLogin} className="border   hover:bg-color_hover w-full   font-bold text-text_tertiary py-4 px-5 rounded mt-5 ">
                    {" "}
                    Log In{" "}
                  </button>
              </div>
            </form>
          </div>
        </div>
      </main>
    </div>
  );
};

export default LoginPage;
