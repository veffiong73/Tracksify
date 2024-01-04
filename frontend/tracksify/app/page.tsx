"use client";
import Logo from "@/components/logo";
import { useRouter } from "next/navigation";

export default function Home() {
  const router = useRouter();

  return (
    <main className="md:flex   ">
      <div className="md:w-1/2  ">
        <div className="bg-background_foreground h-screen">
          <div className="p-4">
            <Logo />
          </div>
          <div className="flex items-center justify-center py-40">
            <h1 className=" text-text_tertiary font-bold text-5xl font-work-sans mb-3 leading-tight">
              Elevate your
              <br /> Productivity with
              <br />{" "}
              <span className=" font-bold text-text_secondary">Tracksify</span>
            </h1>
          </div>
          <button
            onClick={() => router.push("/login")}
            className="md:hidden  flex border  text-text_secondary px-16 py-4  hover:text-white  hover:bg-blue-400   outline rounded "
          >
            Get Started
          </button>
        </div>
      </div>

      <div className="w-1/2 hidden md:flex justify-center items-center">
        <button
          onClick={() => router.push("/login")}
          className="border text-text_secondary px-16 py-4  hover:text-white  hover:bg-blue-400   outline rounded "
        >
          {" "}
          Get Started{" "}
        </button>
      </div>
    </main>
  );
}
