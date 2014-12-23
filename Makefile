all: release

release:
	xbuild /p:Configuration="Release" GrabTheMoment.sln
	mkdir -p build
	cp -r GrabTheMoment/bin/Release build/Release
	rm -rf GrabTheMoment/bin/Release
	@echo Build finished, see \'build/Release\' folder.

debug:
	xbuild /p:Configuration="Debug" GrabTheMoment.sln
	mkdir -p build
	cp -r GrabTheMoment/bin/Debug build/Debug
	rm -rf GrabTheMoment/bin/Debug
	@echo Build finished, see \'build/Debug\' folder.

clean:
	rm -rf build

.PHONY: all release debug clean
